using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace UnityEngine.Experimental.Rendering.HDPipeline
{
    public class VolumetricLightingController : VolumeComponent
    {
        [Tooltip("Distance (in Unity units) from the Camera's Near Clipping Plane to the back of the Camera's volumetric lighting buffer.")]
        public MinFloatParameter depthExtent = new MinFloatParameter(64.0f, 0.1f);
        [Tooltip("Controls the distribution of slices along the Camera's focal axis. 0 is exponential distribution and 1 is linear distribution.")]
        [FormerlySerializedAs("depthDistributionUniformity")]
        public ClampedFloatParameter sliceDistributionUniformity = new ClampedFloatParameter(0.75f, 0, 1);
    }
} // UnityEngine.Experimental.Rendering.HDPipeline
