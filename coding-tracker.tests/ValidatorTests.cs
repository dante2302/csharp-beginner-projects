using Xunit;

namespace coding_tracker.tests
{
    public class ValidatorTests
    {
        [Fact]
        public void Add_TestBasicDate()
        {
            // Arrange
            bool expected = true;
            string dateToValidate = "12/12/2012";

            // Act
            bool actual = Validator.ValidateDate(dateToValidate);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
