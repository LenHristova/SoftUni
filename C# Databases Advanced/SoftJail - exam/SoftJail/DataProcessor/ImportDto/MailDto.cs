﻿namespace SoftJail.DataProcessor.ImportDto
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Text;
		
    public class MailDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z 0-9]+ str\\.$")]
        public string Address { get; set; }
    }
}
