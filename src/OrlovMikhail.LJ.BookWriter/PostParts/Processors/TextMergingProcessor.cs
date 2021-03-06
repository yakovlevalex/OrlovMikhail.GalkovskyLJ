﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrlovMikhail.LJ.BookWriter
{
    public class TextMergingProcessor : ProcessorBase
    {
        protected internal override void ProcessInternal(List<IPostPart> items)
        {
            for(int i = 0; i < items.Count; i++)
            {
                RawTextPostPart rtpp = items[i] as RawTextPostPart;
                if(rtpp == null)
                    continue;

                // Merge into single one.
                while(i < items.Count - 1)
                {
                    RawTextPostPart next = items[i + 1] as RawTextPostPart;
                    if(next == null)
                        break;

                    // Join text, remove next item.
                    rtpp.Text = rtpp.Text + next.Text;
                    items.RemoveAt(i + 1);
                }
            }
        }
    }
}
