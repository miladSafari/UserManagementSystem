using System.Xml.Linq;
using UserManagementSystem.Domain;

namespace UserManagementSystem.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void User_Constructor_Should_be_Create_Correctly()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string name = "Milad";
            string family = "Safari";
            string nationalCode = "0016525507";
            DateTime birthDate = DateTime.Now;

            //Act
            User user = new User(id, name, family, nationalCode, birthDate);

            //Assert
            Assert.Equal(id, user.Id);
            Assert.Equal(name, user.Name);
            Assert.Equal(family, user.Family);
            Assert.Equal(nationalCode, user.NationalCode);
            Assert.Equal(birthDate.ToShortDateString(), user.BirthDate.ToShortDateString());
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData(null)]
        public void User_Name_Must_Be_Required(string? name)
        {
            //Arrange && Act && Assert
            Assert.Throws<ArgumentException>(()=>new User(Guid.NewGuid(), name,"Safari","0016525507", DateTime.Now));

        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData(null)]
        public void User_Family_Must_Be_Required(string? family)
        {
            //Arrange && Act && Assert
            Assert.Throws<ArgumentException>(() => new User(Guid.NewGuid(), "Milad", family, "0016525507", DateTime.Now));
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData(null)]
        public void User_NationalCode_Must_Be_Required(string? nationalCode)
        {
            //Arrange && Act && Assert
            Assert.Throws<ArgumentException>(() => new User(Guid.NewGuid(), "Milad", "Safari", nationalCode, DateTime.Now));
        }
        [Fact]
        public void ChangeName_Should_Work_Correctly()
        {
            //Arrange
            string newName = "  Ali  ";
            User user = new User(Guid.NewGuid(), "Milad", "Safari", "0016525507", DateTime.Now);

            //Act
            user.ChangeName(newName);

            //Assert
            Assert.Equal("Ali", user.Name);
        }
        [Fact]
        public void ChangeFamily_Should_Work_Correctly()
        {
            //Arrange
            string newFamily = "  Jafari  ";
            User user = new User(Guid.NewGuid(), "Milad", "Safari", "0016525507", DateTime.Now);

            //Act
            user.ChangeFamily(newFamily);

            //Assert
            Assert.Equal("Jafari", user.Family);
        }
        [Fact]
        public void ChangeNationalCode_Should_Work_Correctly()
        {
            //Arrange
            string newNationalCode = "  0016525508  ";
            User user = new User(Guid.NewGuid(), "Milad", "Safari", "0016525507", DateTime.Now);

            //Act
            user.ChangeNationalCode(newNationalCode);

            //Assert
            Assert.Equal("0016525508", user.NationalCode);
        }
        //Trim
    }
}
