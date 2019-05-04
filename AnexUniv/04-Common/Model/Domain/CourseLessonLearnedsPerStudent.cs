using Common;
using Model.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class CourseLessonLearnedsPerStudent
    {
        public int Id { get; set; }

        public LessonPerCourse Lesson { get; set; }
        public int LessonId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
