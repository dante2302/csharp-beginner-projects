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
            string[] datesForValidation = ["132/123/2002", "123/01/2002", "12/01/2002", "01/01/200002", "001/001/200002"];

            foreach(string date in datesForValidation)
            {
                bool actual = Validator.ValidateDate(date);
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void ValidateDate_ShouldValidateNonExistentDate()
        {
            bool expected = false;
            string dateToValidate = "31/02/2022";

            bool actual = Validator.ValidateDate(dateToValidate);

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void ValidateTime_ShouldValidateBasicTime()
        {
            bool expected = true;
            string time = "12/24";

            bool actual = Validator.ValidateTime(time);

            Assert.Equal(expected, actual);
        }
    }
}
