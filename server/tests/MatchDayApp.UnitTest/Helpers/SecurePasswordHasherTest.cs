using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Common.Helpers;
using Xunit;

namespace MatchDayApp.UnitTest.Helpers
{
    [Trait("Helpers","PasswordHasher")]
    public class SecurePasswordHasherTest
    {
        private const int _saltSize = 8;

        [Fact]
        public void CreateSalt_SecurePasswordHasher_CreateValidSalt()
        {
            string salt = string.Empty;

            salt = SecurePasswordHasher.CreateSalt(_saltSize);

            salt.Should()
                .NotBeNullOrEmpty()
                .And.HaveLength(12)
                .And.NotMatchRegex("^[A-Z][a-zA-Z]*$");
        }

        [Theory]
        [InlineData("teste")]
        [InlineData("10#ASDMN3233_")]
        [InlineData("RaNdOmPwD123")]
        public void GenerateHash_SecurePasswordHasher_GenerateValidHash(string pwd)
        {
            string hash = string.Empty;
            string salt = SecurePasswordHasher.CreateSalt(_saltSize);

            hash = SecurePasswordHasher.GenerateHash(pwd, salt);

            hash.Should()
                .NotBeNullOrEmpty()
                .And.HaveLength(44)
                .And.NotMatchRegex("^[A-Z][a-zA-Z]*$");
        }

        [Theory]
        [InlineData("TesTEE@123")]
        [InlineData("PWDword2020#")]
        [InlineData("HaShEdPwD7820=")]
        public void AreEqual_SecurePasswordHasher_ValidatePasswordHashIfAreEquals(string pwd)
        {
            string pwdSalt = SecurePasswordHasher.CreateSalt(_saltSize);
            string pwdHashed = SecurePasswordHasher.GenerateHash(pwd, pwdSalt);

            bool isValidPassword = SecurePasswordHasher.AreEqual(pwd, pwdHashed, pwdSalt);

            isValidPassword.Should().BeTrue();
        }

        [Theory]
        [InlineData("TesTEE@123")]
        [InlineData("PWDword2020#")]
        [InlineData("HaShEdPwD7820=")]
        public void AreEqual_SecurePasswordHasher_InvalidatePasswordHashIfNotAreEquals(string pwd)
        {
            string invalidPwd = new Faker().Internet.Password();
            string pwdSalt = SecurePasswordHasher.CreateSalt(_saltSize);
            string pwdHashed = SecurePasswordHasher.GenerateHash(pwd, pwdSalt);

            bool isValidPassword = SecurePasswordHasher.AreEqual(invalidPwd, pwdHashed, pwdSalt);

            isValidPassword.Should().BeFalse();
        }
    }
}
