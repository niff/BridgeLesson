﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BridgeLesson.Models
{
    [DebuggerDisplay("Name = {Name}, BiddingSystemSequences = {BiddingSystemSequences.Count}")]
    public class BiddingSystem : BaseEntity
    {
        public string Name { get; set; }
        public string Description{ get; set; }

        public virtual IList<BiddingSystemSequence> BiddingSystemSequences { get; set; }
              
        public BiddingSystemSequence AddBiddingSequence(BiddingSequence biddingSequence)
        {
            if (this.BiddingSystemSequences == null)
                this.BiddingSystemSequences = new List<BiddingSystemSequence>();

            var biddingSystemSequence = new BiddingSystemSequence(this, biddingSequence);
            this.BiddingSystemSequences.Add(biddingSystemSequence);
            return biddingSystemSequence;

        }

        internal void RemoveBiddingSequence(long biddingSequenceId)
        {
            var biddingSystemSequence = this.BiddingSystemSequences.FirstOrDefault(bss => bss.BiddingSequence.Id == biddingSequenceId);
            if (biddingSystemSequence != null)
                this.BiddingSystemSequences.Remove(biddingSystemSequence);
        }
    }
}