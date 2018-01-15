using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iKnow.Models {
    public class ImageFileTypesAttribute : ValidationAttribute {
        private readonly List<string> _types;

        public ImageFileTypesAttribute(string types) {
            _types = types.Split(',').ToList();
        }

        public override bool IsValid(object value) {
            if (value == null) return true;

            var fileExt = System.IO
                                .Path
                                .GetExtension((value as
                                         HttpPostedFileBase).FileName).Substring(1);
            return _types.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
        }

        public override string FormatErrorMessage(string name) {
            return $"Invalid file type. Only {string.Join(", ", _types)} are supported.";
        }
    }
}