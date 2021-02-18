namespace CCC_API.Data.TestDataObjects
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Cision.PublicRelations.Api.Models.Distribution")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Cision.PublicRelations.Api.Models.Distribution", IsNullable = false)]
    public partial class UploadingFile
    {

        private string sizeField;

        private string storageIDField;

        private string uploadTokenField;

        /// <remarks/>
        public string Size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }

        /// <remarks/>
        public string StorageID
        {
            get
            {
                return this.storageIDField;
            }
            set
            {
                this.storageIDField = value;
            }
        }

        /// <remarks/>
        public string UploadToken
        {
            get
            {
                return this.uploadTokenField;
            }
            set
            {
                this.uploadTokenField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Cision.PublicRelations.Api.Models.Distribution")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Cision.PublicRelations.Api.Models.Distribution", IsNullable = false)]
    public partial class NewDataSet
    {

        private UploadingFile[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("UploadingFile")]
        public UploadingFile[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
}
