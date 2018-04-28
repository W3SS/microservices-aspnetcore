namespace locationservice.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using locationservice.Models;

    public class InMemoryLocationRecordRepository : ILocationRecordRepository
    {
        private static Dictionary<Guid, SortedList<long, LocationRecord>> locationRecords;

        public InMemoryLocationRecordRepository() {
            if (locationRecords == null) {
                locationRecords = new Dictionary<Guid, SortedList<long, LocationRecord>>();
            }
        }
        
        public LocationRecord Add(LocationRecord locationRecord)
        {
            var memberRecords = this.getMemberRecords(locationRecord.MemberId);

            memberRecords.Add(locationRecord.Timestamp, locationRecord);
            return locationRecord;
        }

        public ICollection<LocationRecord> AllForMember(Guid memberId)
        {
            var memberRecords = this.getMemberRecords(memberId);
            return memberRecords.Values.Where( l => l.MemberId == memberId).ToList();
        }

        public LocationRecord Delete(Guid memberId, Guid recordId)
        {
            var memberRecords = this.getMemberRecords(memberId);
            var lr = memberRecords.Values.FirstOrDefault(l => l.Id == recordId);
            
            if (lr != null) {
                memberRecords.Remove(lr.Timestamp);
            }

            return lr;
        }

        public LocationRecord Get(Guid memberId, Guid recordId)
        {
            var memberRecords = this.getMemberRecords(memberId);

            var lr = memberRecords.Values.FirstOrDefault(l => l.Id == recordId);
            return lr;
        }

        public LocationRecord Update(LocationRecord locationRecord)
        {
            return this.Delete(locationRecord.MemberId, locationRecord.Id);
        }

        public LocationRecord GetLatestForMember(Guid memberId) {
            var memberRecords = this.getMemberRecords(memberId);

            var lr = memberRecords.Values.LastOrDefault();
            return lr;
        }

        private SortedList<long, LocationRecord> getMemberRecords(Guid memberId) {
            if (!locationRecords.ContainsKey(memberId)) {
                locationRecords.Add(memberId, new SortedList<long, LocationRecord>());
            }

            var list = locationRecords[memberId];
            return list;
        }
    }
}