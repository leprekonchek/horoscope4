using System;

namespace _04_Lopukhina.Tools.Exceptions
{
    class OldToBeAliveException : Exception
    {
        public OldToBeAliveException(int age) : base($"Hmm, you are too old to be alive!\n The oldest man on the Erath was 122, and you are {age}. \n If you are alive, than congratulation and I'm pressing F")
        { }
    }

    class NotBornException : Exception
    {
        public NotBornException() : base("Hey, you haven't born yet!")
        { }
    }

    class NotValidEmailException : Exception
    {
        public NotValidEmailException(string email) : base($"You must have missed something in your email address {email}. \n Check it one more time!")
        { }
    }
}
