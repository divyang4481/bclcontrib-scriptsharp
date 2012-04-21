namespace IDT4.Engine.CM
{
    // contact type
    public enum ContactType
    {
        CONTACT_NONE,			// no contact
        CONTACT_EDGE,			// trace model edge hits model edge
        CONTACT_MODELVERTEX,	// model vertex hits trace model polygon
        CONTACT_TRMVERTEX		// trace model vertex hits model polygon
    }
}
