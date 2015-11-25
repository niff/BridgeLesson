﻿using LeadLesson.Models;
using LeadLesson.Utils;
using LeadLesson.ViewModels;
using System.Web.Http;
using BridgeLesson.Models;

namespace LeadLesson.Controllers
{
    public class BiddingSystemController : ApiController
    {
        private readonly BiddingRepository biddingRepository = new BiddingRepository();

        // GET: api/BiddingSystem
        public IHttpActionResult Get()
        {
            var biddingSequences = biddingRepository.GetBiddingSystems();
            return Ok(biddingSequences);
        }

        [HttpGet]
        [Route("api/BiddingSystem/{id}")]
        // GET: api/BiddingSystem/5
        public IHttpActionResult Get(int id)
        {
            var biddingSequences = this.biddingRepository.GetBiddingSequencesBySystem(id);
            return Ok(biddingSequences);
        }

        [HttpGet]
        [Route("api/BiddingSystem/AsParentChild/{id}")]
        // GET: api/BiddingSystem/5
        public IHttpActionResult GetAsParentChild(int id)
        {
            var biddingSequences = this.biddingRepository.GetBiddingSequencesBySystem(id);
            var system = this.biddingRepository.GetBiddingSystem(id);

            var rootBid = BiddingConverter.ConvertWithRoot(biddingSequences);
            rootBid.BidSymbol = system.Name;
            var bidsAsParentChild = new BiddingSystemGetAsParentChildViewModel { RootBid = rootBid };
            return Ok(bidsAsParentChild);
        }
        
        // POST: api/BiddingSystem
        [HttpPost]
        [Route("api/BiddingSystem/{systemToCopyId}")]
        public BiddingSystem Post([FromBody]BiddingSystem biddingSystem, int? systemToCopyId = null)
        {
            return biddingRepository.CreateBiddingSystem(biddingSystem, systemToCopyId);
        }

        // PUT: api/BiddingSystem/5
        public BiddingSystem Put(int id, [FromBody]BiddingSystem biddingSystem)
        {
            return biddingRepository.CreateBiddingSystem(biddingSystem);
        }

        // DELETE: api/BiddingSystem/5
        public void Delete(int id)
        {

        }

        [HttpPut]
        [Route("api/BiddingSystem/AddBiddingSequenceToSystem/{biddingSystemId}/{biddingSequenceId}")]
        public IHttpActionResult AddBiddingSequenceToSystem(long biddingSystemId, long biddingSequenceId)
        {
            var biddingSystemSequence = this.biddingRepository.AddBiddingSequenceToSystem(biddingSystemId, biddingSequenceId);
            return Ok(biddingSystemSequence.BiddingSequence);
        }

        [HttpDelete]
        [Route("api/BiddingSystem/RemoveBiddingSequenceFromSystem/{biddingSystemId}/{biddingSequenceId}")]
        public void RemoveBiddingSequenceFromSystem(long biddingSystemId, long biddingSequenceId)
        {
            this.biddingRepository.RemoveBiddingSequence(biddingSystemId, biddingSequenceId);
        }

    }
}