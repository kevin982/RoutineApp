using System;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        //[Fact]
        //public void Test1()
        //{
            
        //    Assert.True(true, "Tiene que retornar true");
        //}


        [Theory]
        [InlineData(17, false)]
        [InlineData(45, true)]

        public void VerificarEdadEsCorrecta(int edad, bool expected)
        {
            //Arrange
 
            Persona persona = new () { Edad = edad };

            //Act 

            bool result = persona.EsMayorDeEdad();

            //Assert

            Assert.Equal(expected, result);

            

        }


    }
}
