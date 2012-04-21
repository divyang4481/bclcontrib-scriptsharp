namespace IDT4.Engine.CM
{
    internal partial class CollisionModelManager
    {
        public int Contacts(ContactInfo contacts, int maxContacts, ref idVec3 start, ref idVec6 dir, float depth, idTraceModel trm, ref idMat3 trmAxis, int contentMask, CmHandle model, ref idVec3 origin, ref idMat3 modelAxis)
        {
            // same as Translation but instead of storing the first collision we store all collisions as contacts
            this.getContacts = true;
            this.contacts = contacts;
            this.maxContacts = maxContacts;
            this.numContacts = 0;
            idVec3 end = start + dir.SubVec3(0) * depth;
            Trace results;
            Translation(out results, start, end, trm, trmAxis, contentMask, model, origin, modelAxis);
            if (dir.SubVec3(1).LengthSqr() != 0.0f)
            { 
                // FIXME: rotational contacts 
            }
            this.getContacts = false;
            this.maxContacts = 0;
            return this.numContacts;
        }
    }
}