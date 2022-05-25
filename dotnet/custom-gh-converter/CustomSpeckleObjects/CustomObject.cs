using Speckle.Core.Models;
using Objects.Geometry;
using Speckle.Core.Kits;

namespace CustomSpeckleObjects
{
    public class FancyObject : Base
    {
        [DetachProperty]
        public Plane? Origin { get; set; }

        public FancyObject()
        {

        }

        [SchemaInfo("Fancy Object", "A fancy object based on a plane.")]
        public FancyObject(Plane plane)
        {
            Origin = plane;
        }
    }

}
