using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Core.Entities
{
    public class Musics : BaseEntity
    {
        public string Name { get; set; }
        public int AlbumId { get; set; }
        public double Storage { get; set; }
        public Album Album { get; set; }
    }
}
