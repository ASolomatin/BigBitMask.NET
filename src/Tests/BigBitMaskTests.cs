using System;
using BigBitMask.NET;
using NExpect;
using NUnit.Framework;

using static NExpect.Expectations;

namespace Tests
{
    [TestFixture]
    public class BigBitMaskTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCtor()
        {
            BitMask bitMask;

            Expect(() => bitMask = new BitMask()).Not.To.Throw();
            Expect(() => bitMask = new BitMask("AB@CD")).To.Throw<FormatException>();
            Expect(() => bitMask = new BitMask("AB2CD")).Not.To.Throw();
        }

        [Test]
        public void TestGetter()
        {
            var bitMask = new BitMask();

            Expect(() => bitMask[-1]).To.Throw<ArgumentOutOfRangeException>();
            Expect(() => bitMask[0]).Not.To.Throw();
            Expect(() => bitMask[1]).Not.To.Throw();
            Expect(() => bitMask[1000]).Not.To.Throw();

            Expect(bitMask[0]).To.Be.False();
            Expect(bitMask[1]).To.Be.False();
            Expect(bitMask[1000]).To.Be.False();

            bitMask = new BitMask("CE");

            Expect(bitMask[0]).To.Be.False();
            Expect(bitMask[2]).To.Be.False();
            Expect(bitMask[1000]).To.Be.False();

            Expect(bitMask[1]).To.Be.True();
            Expect(bitMask[8]).To.Be.True();
        }

        [Test]
        public void TestSetter()
        {
            var bitMask = new BitMask();

            Expect(() => bitMask[-1] = true).To.Throw<ArgumentOutOfRangeException>();
            Expect(() => bitMask[0] = true).Not.To.Throw();
            Expect(() => bitMask[1] = true).Not.To.Throw();
            Expect(() => bitMask[1000] = true).Not.To.Throw();

            Expect(bitMask[0]).To.Be.True();
            Expect(bitMask[1]).To.Be.True();
            Expect(bitMask[1000]).To.Be.True();

            Expect(() => bitMask[1000] = false).Not.To.Throw();
            Expect(bitMask[1000]).To.Be.False();
        }

        [Test]
        public void TestToString()
        {
            var bitMask = new BitMask("CE");
            Expect(bitMask.ToString()).To.Equal("CE");

            bitMask = new BitMask("CEAAAA");
            Expect(bitMask.ToString()).To.Equal("CE");

            bitMask[1000] = true;
            Expect(bitMask.ToString()).To.Equal("CEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQ");

            bitMask[1000] = false;
            Expect(bitMask.ToString()).To.Equal("CE");
        }

        [Test]
        public void TestBitCapacityProperty()
        {
            var bitMask = new BitMask("CE");
            Expect(bitMask.BitsCapacity).To.Equal(12);

            bitMask[1000] = true;
            Expect(bitMask.BitsCapacity).To.Equal(1002);

            bitMask[2000] = false;
            Expect(bitMask.BitsCapacity).To.Equal(1002);
        }
    }
}