﻿using System.ComponentModel.DataAnnotations;

namespace CheckListApi.Models
{
    public class Task
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int BoardId { get; set; }
        public TaskBoard Board { get; set; }


        [MaxLength(25)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public bool InProgress { get; set; } = false;
        public bool IsDone { get; set; } = false;
    }
}
