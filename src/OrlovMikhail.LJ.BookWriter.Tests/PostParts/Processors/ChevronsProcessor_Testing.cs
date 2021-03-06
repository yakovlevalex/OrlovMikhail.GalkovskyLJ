﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OrlovMikhail.LJ.BookWriter.Tests
{
    [TestFixture]
    public class ChevronsProcessor_Testing
    {
        [TestCase(@"--RAM--")]
        [TestCase(@"//RAM//")]
        public void ConvertsSpecialQuotations(string input)
        {
            PostPartBase[] parts =
            {
                new RawTextPostPart(input),
            };

            PostPartBase[] expected =
            {
                new ParagraphStartPart(1),
                new RawTextPostPart("RAM")
            };

            Check(parts, expected);
        }

        [Test]
        public void RemovesChevrons()
        {
            RawTextPostPart[] rtpps = new[] { new RawTextPostPart(">>"), new RawTextPostPart("A") };
            int count = ChevronsProcessor.RemoveChevrons(rtpps);
            Assert.AreEqual(2, count);
            Assert.IsEmpty(rtpps[0].Text);
            Assert.AreEqual("A", rtpps[1].Text);
        }

        [Test]
        public void DoubleQuotation()
        {
            PostPartBase[] parts =
            {
                new RawTextPostPart(">A"),  LineBreakPart.Instance,
                new RawTextPostPart(">B"),  LineBreakPart.Instance,
                new RawTextPostPart(">>C"), LineBreakPart.Instance,
                new RawTextPostPart(">D"),
            };

            PostPartBase[] expected =
            {
                new ParagraphStartPart(1),
                new RawTextPostPart("A"),
                new RawTextPostPart(" "),
                new RawTextPostPart("B"),
                new ParagraphStartPart(2),
                new RawTextPostPart("C"),
                new ParagraphStartPart(1),
                new RawTextPostPart("D"),
            };

            Check(parts, expected);
        }

        [Test]
        public void SimplestCase()
        {
            PostPartBase[] parts =
            {
                new RawTextPostPart(">A")
            };

            PostPartBase[] expected =
            {
                new ParagraphStartPart(1),
                new RawTextPostPart("A")
            };

            Check(parts, expected);
        }

        [Test]
        public void PreservesLineBreaks()
        {
            PostPartBase[] parts =
            {
                new RawTextPostPart(">A"),
                LineBreakPart.Instance,
                new RawTextPostPart(">B"),
                LineBreakPart.Instance,
                new RawTextPostPart("C"),
                LineBreakPart.Instance,
                new RawTextPostPart("D"),
            };

            PostPartBase[] expected =
            {
                new ParagraphStartPart(1),
                new RawTextPostPart("A"),
                new RawTextPostPart(" "),
                new RawTextPostPart("B"),
                new ParagraphStartPart(0),
                new RawTextPostPart("C"),
                LineBreakPart.Instance,
                new RawTextPostPart("D"),
            };

            Check(parts, expected);
        }

        [Test]
        public void RemovesFormatting()
        {
            PostPartBase[] parts =
            {
                LineBreakPart.Instance,
                ItalicStartPart.Instance,
                new RawTextPostPart(">B"),  LineBreakPart.Instance,
                new RawTextPostPart(">C"),  LineBreakPart.Instance,
                new RawTextPostPart(">>D"),
                ItalicEndPart.Instance,
                LineBreakPart.Instance,
            };

            PostPartBase[] expected =
            {
                new ParagraphStartPart(1),
                new RawTextPostPart("B"),
                new RawTextPostPart(" "),
                new RawTextPostPart("C"),
                new ParagraphStartPart(2),
                new RawTextPostPart("D"),
                new ParagraphStartPart(0),
           };

            Check(parts, expected);
        }

        private void Check(PostPartBase[] parts, PostPartBase[] expected)
        {
            IProcessor cp = new ChevronsProcessor();
            List<IPostPart> processed = cp.Process(parts);
            CollectionAssert.AreEqual(expected, processed);
        }
    }
}
