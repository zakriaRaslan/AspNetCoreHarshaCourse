namespace CRUDTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestAddMethoud()
        {
            //Arrang
            MyMath myMath = new MyMath();
            int input1 = 10, input2 = 15;
            int exepected = 25;

            //Act
            int actual = myMath.Add(input1, input2);

            //Assert
            Assert.Equal(exepected, actual);
        }
    }
}