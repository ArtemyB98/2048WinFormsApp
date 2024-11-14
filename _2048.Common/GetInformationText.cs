using static System.Formats.Asn1.AsnWriter;

namespace _2048.Common
{
    public class GetInformationText()
    {
        public static string Rules()
        {
            return "1. Slide by arrows\n" +
                "2. Sum scores\n" +
                "3. GameOver is over if you better can't slide";
        }
        public static string GameOver(int scores)
        {
            return $"GameOver over\nYour score: {scores}";
        }
    }
}