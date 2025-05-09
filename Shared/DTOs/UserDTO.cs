﻿using System.ComponentModel;

namespace Shared.DTOs
{
    public class UserDTO
    {
        public Guid UserId { get; set; }

        public int RoleId { get; set; }

        public string FullName { get; set; } = null!;

        public string Avatar { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string? Password { get; set; }

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public string? IdentificationNumber { get; set; }

        public string? Bio { get; set; }
        [DefaultValue(false)]
        public bool? UpToAuthor { get; set; }

        public int? ReportCount { get; set; }
        [DefaultValue(false)]
        public bool? IsDeleted { get; set; }
    }
}
