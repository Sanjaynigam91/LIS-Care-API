using LISCareDTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LISCareUtility
{
    public class Common
    {
        private static readonly string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        public Common(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;

        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        //Decode the Base64 Image
        public static byte[] DecodeBase64(string base64String)
        {
            // Remove the "data:image/*;base64," part if it's included
            string base64Data = base64String.Split(',')[1];
            return Convert.FromBase64String(base64Data);
        }

        public static string PrefixOfBase64(string base64String)
        {
            // Remove the "data:image/*;base64," part if it's included
            string dataUrlPrefix = base64String.Split(',')[0];
            return dataUrlPrefix;
        }

        //Save the Image to a Folder
        public static string SaveImageToFile(byte[] imageData, string folderPath, string fileName)
        {
            // Ensure the folder exists
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            // Write the file
            File.WriteAllBytes(filePath, imageData);

            return filePath;
        }

        public static string GetFileNameFromBase64(string base64)
        {
            string filename = string.Empty;
            var match = Regex.Match(base64, @"data:(?<mime>[^;]+);base64,(?<data>.*)");
            if (match.Success)
            {
                string mimeType = match.Groups["mime"].Value;
                string base64Data = match.Groups["data"].Value;

                // Assuming you want to derive a filename from MIME type
                string extension = mimeType switch
                {
                    "image/png" => "png",
                    "image/jpeg" => "jpg",
                    "image/gif" => "gif",
                    _ => "bin"
                };
                // Generate a unique file name
                string newfileName = Guid.NewGuid().ToString();
                filename = newfileName + "." + $"{extension}";
                //  filename = $"file.{extension}";

                // Decode base64 data
                byte[] fileData = Convert.FromBase64String(base64Data);
            }
            return filename;
        }

        public static string ConvertImageToBase64(string imagePath)
        {
            // Read the image file into a byte array
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Convert the byte array to a Base64 string
            string base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }

        // To generate token
        public static string GenerateToken(string? email, string? role, IConfiguration _configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,email),
                new Claim(ClaimTypes.Role,role)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GeneratePartnerId()
        {
            Random random = new Random();
            int partnerCode = random.Next(100000, 999999);
            string partnerId = partnerCode.ToString();
            partnerId = "P" + partnerId;
            return partnerId.ToString();
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    var data = new byte[length];
            //    rng.GetBytes(data);

            //    var stringBuilder = new StringBuilder(length);

            //    foreach (byte b in data)
            //    {
            //        stringBuilder.Append(Chars[b % Chars.Length]);
            //    }

            //    return stringBuilder.ToString();
            //}
        }

        public string GetFilePath(string fileName)
        {
            string basePath;

            if (_environment.IsDevelopment())
            {
                // Local machine
                basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LabUserLogo");
            }
            else
            {
                // Azure App Service
                basePath = Path.Combine(_environment.WebRootPath, "LabUserLogo");
            }

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            string filePath = Path.Combine(basePath, fileName);
            // Save your file here...
            return filePath;
        }

        public static class NumberToWordsConverter
        {
            private static readonly string[] Units =
            {
                "Zero","One","Two","Three","Four","Five","Six","Seven","Eight","Nine",
                "Ten","Eleven","Twelve","Thirteen","Fourteen","Fifteen","Sixteen",
                "Seventeen","Eighteen","Nineteen"
             };

            private static readonly string[] Tens =
            {
                "Zero","Ten","Twenty","Thirty","Forty","Fifty","Sixty","Seventy","Eighty","Ninety"
            };

            public static string Convert(decimal amount)
            {
                if (amount == 0)
                    return "Zero Only";

                long rupees = (long)amount;
                int paise = (int)((amount - rupees) * 100);

                string words = $"{ConvertToWords(rupees)} Rupees";

                if (paise > 0)
                    words += $" and {ConvertToWords(paise)} Paise";

                return words + " Only";
            }

            private static string ConvertToWords(long number)
            {
                if (number < 20)
                    return Units[number];

                if (number < 100)
                    return Tens[number / 10] + (number % 10 > 0 ? " " + Units[number % 10] : "");

                if (number < 1000)
                    return Units[number / 100] + " Hundred" +
                           (number % 100 > 0 ? " " + ConvertToWords(number % 100) : "");

                if (number < 100000)
                    return ConvertToWords(number / 1000) + " Thousand" +
                           (number % 1000 > 0 ? " " + ConvertToWords(number % 1000) : "");

                if (number < 10000000)
                    return ConvertToWords(number / 100000) + " Lakh" +
                           (number % 100000 > 0 ? " " + ConvertToWords(number % 100000) : "");

                return ConvertToWords(number / 10000000) + " Crore" +
                       (number % 10000000 > 0 ? " " + ConvertToWords(number % 10000000) : "");
            }
        }


    }
}
