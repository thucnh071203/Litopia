using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.EmailService.EmailTemplates
{
    public static class EmailTemplateHelper
    {
        public static string EmailConfirm(string username, string content)
        {
            return $@"<!DOCTYPE html>
            <html lang='en'>
            <head>...</head>
            <body>
                <div class='container'>
                    <h1>Upgrade to Author</h1>
                    <div class='content'>
                        <p>Hello {username},</p>
                        {content}
                        <p>Best regards,<br/>Litopia Team</p>
                    </div>
                </div>
            </body>
            </html>";
        }
    }
}
