namespace ProDat.Web2.ViewModels
{
    public class UnassignedAtt
    {
        // will need to store these in db somewhere.
        public int KeyListId { get; set; }

        public int KeyListName { get; set; }

        public string EngClassName { get; set; }

        public string EngClassDesc { get; set; }

        public string EngDataCodeName { get; set; }

        public int EngDataClassId { get; set; }

        public string EngDataClassName { get; set; }

        public string EngDataClassDesc { get; set; }

        public int height { get; set; }
        public int width { get; set; }
        public int? parent { get; set; }
        public string customstring { get; set; }

        public string EngDataCodes { get; set; }



    }
}
