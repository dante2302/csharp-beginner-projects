using Xunit;

namespace coding_tracker.tests
{
    public class ValidatorTests
    {
        [Fact]
        public void ValidateDate_ShouldValidateBasicDate()
        {
            // Arrange
            bool expected = true;
            string dateToValidate = "12/12/2012";

            // Act
            bool actual = Validator.ValidateDate(dateToValidate);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateDate_ShouldValidateWrongFormatDate()
        {
            bool expected = false;
            string dateToValidate = "132/123/2002";

            bool actual = Validator.ValidateDate(dateToValidate);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateDate_ShouldValidateNonExistentDate()
        {
            bool expected = false;
            string dateToValidate = "31/02/2022";

            bool actual = Validator.ValidateDate(dateToValidate);

            Assert.Equal(expected, actual);
        }
    }
}
