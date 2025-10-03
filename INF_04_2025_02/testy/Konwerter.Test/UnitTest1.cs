using Xunit;
using konsola;

namespace Konwerter.Test
{
    public class UnitTest1
    {
        [Fact]
        public void MetryNaKilometryTest()
        {
            double wynik = Program.Konwertuj(1, "m", "km", 1500);
            Assert.InRange(wynik, 1.499, 1.501);
        }
        [Fact]
        public void MileNaMetryTest()
        {
            double wynik = Program.Konwertuj(1, "mi", "m", 1);
            Assert.InRange(wynik, 1609.33, 1609.35);
        }
        [Fact]
        public void KilogramyNaFuntyTest()
        {
            double wynik = Program.Konwertuj(2, "kg", "lb", 1);
            Assert.InRange(wynik, 2.2045, 2.2047);
        }
        [Fact]
        public void MiligramyNaKilogramyTest()
        {
            double wynik = Program.Konwertuj(2, "mg", "kg", 1000000);
            Assert.InRange(wynik, 0.999, 1.001);
        }
        [Fact]
        public void CelsiusNaFahrenheitTest()
        {
            double wynik = Program.Konwertuj(3, "C", "F", 100);
            Assert.InRange(wynik, 211.9, 212.1);
        }
        [Fact]
        public void FahrenheitNaCelsiusTest()
        {
            double wynik = Program.Konwertuj(3, "F", "C", 32);
            Assert.InRange(wynik, -0.01, 0.01);
        }
        [Fact]
        public void CelsiusNaKelvinTest()
        {
            double wynik = Program.Konwertuj(3, "C", "K", 0);
            Assert.InRange(wynik, 273.14, 273.16);
        }
        [Fact]
        public void NiepoprawnaJednostkaDlugoscTest()
        {
            double wynik = Program.Konwertuj(1, "m", "blad", 100);
            Assert.Equal(-1.0, wynik);
        }
        [Fact]
        public void NiepoprawnaJednostkaTemperaturaTest()
        {
            double wynik = Program.Konwertuj(3, "C", "blad", 100);
            Assert.Equal(-1.0, wynik);
        }
    }
}
