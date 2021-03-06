﻿using OrlovMikhail.LJ.Grabber;
using System;
using System.IO.Abstractions;
using OrlovMikhail.LJ.BookWriter;

namespace OrlovMikhail.LJ.BookWriter
{
    public interface IBookWriter : IDisposable
    {
        void EntryPageBegin();
        void EntryPageEnd();

        void CommentsBegin();
        void CommentsEnd();

        void ThreadBegin();
        void ThreadEnd();

        void EntryEnd();

        void CommentEnd();

        void EntryHeader(Entry e, string posterUserpicRelativeLocation);

        void CommentHeader(Comment c, string commentUserpicRelativeLocation);

        void WritePart(IPostPart ppb);
    }
}