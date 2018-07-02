using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FunctionalLink;

namespace FunctionalLink.Tests
{
    [TestClass]
    public class ScratchTests
    {
        [TestMethod]
        public void X()
        {

        }

        public class RankedCard { }

        public class Two : RankedCard { }
        public class Three : RankedCard { }
        public class Four : RankedCard { }
        public class Five : RankedCard { }
        public class Six : RankedCard { }
        public class Seven : RankedCard { }
        public class Eight : RankedCard { }
        public class Nine : RankedCard { }
        public class Ten : RankedCard { }
        public class Jack : RankedCard { }
        public class Queen : RankedCard { }
        public class King : RankedCard { }
        public class Ace : RankedCard { }
        
        public class Joker { }

        public class Card : Union<RankedCard, Joker>
        {
            public Card(RankedCard value) : base(value)
            {
            }

            public Card(Joker valueB) : base(valueB)
            {
            }
        }
    }
}
