using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    /// <summary>
    /// Upload file details.
    /// </summary>
    [MessagePackObject]
    public class UploadFile
    {
        /// <summary>
        /// Gets or sets file name.
        /// </summary>
        [Key(0)]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets file data in bytes.
        /// </summary>
        [Key(1)]
        public byte[] FileBytes { get; set; }

        /// <summary>
        /// Gets or sets uploaded by.
        /// </summary>
        [Key(2)]
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets uploaded date time in IST.
        /// </summary>
        /// <value></value>
        [Key(3)]
        public DateTime UpdatedDateTimeIST { get; set; }
    }
}