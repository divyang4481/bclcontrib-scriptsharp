namespace IDT4.Engine.CM
{
    // contact info
    public class ContactInfo
    {
        public ContactType type;	// contact type
        public idVec3 point;		// point of contact
        public idVec3 normal;		// contact plane normal
        public float dist;			// contact plane distance
        public int contents;		// contents at other side of surface
        public idMaterial material; // surface material
        public int modelFeature;	// contact feature on model
        public int trmFeature;		// contact feature on trace model
        public int entityNum;		// entity the contact surface is a part of
        public int id;				// id of clip model the contact surface is part of
    }
}
