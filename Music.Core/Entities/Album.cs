using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Music.Core.Entities
{
    public class Album : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public List<Musics> Musics { get; set; }
    }
}
