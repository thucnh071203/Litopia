﻿using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.EmailService;
using Shared.EmailService.EmailTemplates;
using Shared.Helpers;
using UserService.Application.Interfaces;

namespace UserService.Controllers
{
    [Route("api/authen")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly PasswordHasher _passwordHasher;
        private readonly IEmailSender _emailSender; // Giả sử bạn có một service

        public AuthenController(IUsersService usersService, PasswordHasher passwordHasher, IEmailSender emailSender)
        {
            _usersService = usersService;
            _passwordHasher = passwordHasher;
            _emailSender = emailSender;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var response = await _usersService.LoginAsync(loginDto);

            if (!response.Success)
                return BadRequest(response); // Trả về DTO với ErrorMessage

            return Ok(response); // Trả về DTO với Token và thông tin khác
        }

        [HttpPost("login-with-google")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] LoginGoogleDTO request)
        {
            var result = await _usersService.LoginWithGoogleAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var result = await _usersService.RegisterAsync(registerDto);

            if (result == null)
                return Conflict("Email or Username already exists and confirmed");

            // Send OTP
            string otp = await _usersService.GenerateOtpAsync(registerDto.Email);
            var content = $"<p>Your OTP is:</p><div class='otp-box'>{otp}</div><p>It is valid for 2 minutes.</p>";
            var html = EmailTemplateHelper.EmailConfirm(registerDto.FullName, content);
            await _emailSender.SendEmailAsync(registerDto.Email, "Confirm Email", html);

            return Ok(result);
        }


        [HttpPut("change-password/{id}")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordDTO changePasswordDto)
        {
            var user = await _usersService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            if (!_passwordHasher.VerifyPassword(changePasswordDto.CurrentPassword, user.Password))
                return BadRequest("Current password is incorrect!");

            user.Password = changePasswordDto.NewPassword;
            await _usersService.UpdateAsync(id, user);
            return Ok("Password changed successfully!");
        }

        [HttpPut("set-password/{id}")]
        public async Task<IActionResult> SetPassword(string id, [FromBody] SetPasswordDTO setPasswordDto)
        {
            var user = await _usersService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            if (user.Otp != "none")
                return BadRequest("Password has been set");

            user.Password = setPasswordDto.NewPassword;
            user.Otp = null;
            await _usersService.UpdateAsync(id, user);
            return Ok("Password set successfully!");
        }


        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.ToEmail) || string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body))
            {
                return BadRequest("Please provide ToEmail, Subject, and Body");
            }

            try
            {
                var content = "<strong>Congratulations! Your account has been upgraded to an Author.</strong>";
                var html = EmailTemplateHelper.EmailConfirm(request.ToEmail, content);
                await _emailSender.SendEmailAsync(request.ToEmail, request.Subject, html);

                return Ok("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Email sending failed: {ex.Message}");
            }
        }

        [HttpPut("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] EmailRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.ToEmail))
            {
                return BadRequest("Please provide ToEmail");
            }

            // Get the OTP from the service
            string otp = await _usersService.GenerateOtpAsync(request.ToEmail);
            if (otp == null)
            {
                return NotFound("User not found");
            }

            // Find the user to get the username for the email template
            var user = await _usersService.GetByEmailAsync(request.ToEmail);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Send OTP via email
            var content = $"<p>Your OTP is:</p><div class='otp-box'>{otp}</div><p>It is valid for 2 minutes.</p>";
            var html = EmailTemplateHelper.EmailConfirm(user.FullName, content);
            await _emailSender.SendEmailAsync(user.Email, "Your OTP Code", html);

            return Ok("OTP sent successfully!");
        }

        [HttpPut("confirm-otp")]
        public async Task<IActionResult> ConfirmOtp([FromBody] ConfirmOtpDTO request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Otp))
            {
                return BadRequest("Please provide Email and OTP");
            }

            bool isValid = await _usersService.ConfirmOtpAsync(request.Email, request.Otp);
            if (!isValid)
            {
                return BadRequest("Invalid or expired OTP, or user not found");
            }

            return Ok("OTP confirmed successfully!");
        }
    }
}
