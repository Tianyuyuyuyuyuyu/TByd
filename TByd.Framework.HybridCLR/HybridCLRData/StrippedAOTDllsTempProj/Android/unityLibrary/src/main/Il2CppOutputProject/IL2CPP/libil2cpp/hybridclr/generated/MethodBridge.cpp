#include <codegen/il2cpp-codegen-metadata.h>
#include "vm/ClassInlines.h"
#include "vm/Object.h"
#include "vm/Class.h"

#include "../metadata/MetadataModule.h"
#include "../metadata/MetadataUtil.h"

#include "../interpreter/MethodBridge.h"
#include "../interpreter/Interpreter.h"
#include "../interpreter/MemoryUtil.h"
#include "../interpreter/InstrinctDef.h"

using namespace hybridclr::interpreter;

//!!!{{CODE
// System.ByReference`1<System.Byte>
struct __struct_123__ {
	intptr_t __0; // _value
};
// System.Span`1<System.Byte>
struct __struct_0__ {
	__struct_123__ __0; // _pointer
	int32_t __1; // _length
};
// System.Collections.Generic.KeyValuePair`2<System.Object,System.Object>
struct __struct_3__ {
	uintptr_t __0; // key
	uintptr_t __1; // value
};
// System.Resources.ResourceLocator
struct __struct_4__ {
	uintptr_t __0; // _value
	int32_t __1; // _dataPos
};
// System.Collections.Generic.KeyValuePair`2<System.Int32,System.Object>
struct __struct_6__ {
	int32_t __0; // key
	uintptr_t __1; // value
};
// System.Collections.Generic.KeyValuePair`2<System.Object,System.Resources.ResourceLocator>
struct __struct_8__ {
	uintptr_t __0; // key
	__struct_4__ __1; // value
};
// System.Collections.Generic.KeyValuePair`2<System.ValueTuple`2<System.Object,System.Object>,System.Object>
struct __struct_9__ {
	__struct_3__ __0; // key
	uintptr_t __1; // value
};
// System.Nullable`1<System.Object>
struct __struct_10__ {
	uint8_t __0; // hasValue
	uintptr_t __1; // value
};
// System.Reflection.CustomAttributeNamedArgument
struct __struct_13__ {
	__struct_3__ __0; // <TypedValue>k__BackingField
	uint8_t __1; // <IsField>k__BackingField
	uintptr_t __2; // <MemberName>k__BackingField
	uintptr_t __3; // _attributeType
	uintptr_t __4; // _lazyMemberInfo
};
// System.ConsoleKeyInfo
struct __struct_15__ {
	uint16_t __0; // _keyChar
	int32_t __1; // _key
	int32_t __2; // _mods
};
// System.Decimal
union __struct_16__ {
	#pragma pack(push, 1)
	struct {   int32_t __0;}; // flags
	#pragma pack(pop)
	struct { int32_t __0_forAlignmentOnly;}; // flags
	#pragma pack(push, 1)
	struct { char __1_offsetPadding[4];  int32_t __1;}; // hi
	#pragma pack(pop)
	struct { int32_t __1_forAlignmentOnly;}; // hi
	#pragma pack(push, 1)
	struct { char __2_offsetPadding[8];  int32_t __2;}; // lo
	#pragma pack(pop)
	struct { int32_t __2_forAlignmentOnly;}; // lo
	#pragma pack(push, 1)
	struct { char __3_offsetPadding[12];  int32_t __3;}; // mid
	#pragma pack(pop)
	struct { int32_t __3_forAlignmentOnly;}; // mid
	#pragma pack(push, 1)
	struct { char __4_offsetPadding[8];  uint64_t __4;}; // ulomidLE
	#pragma pack(pop)
	struct { uint64_t __4_forAlignmentOnly;}; // ulomidLE
};
// System.DateTime
struct __struct_17__ {
	uint64_t __0; // _dateData
};
// System.DateTimeOffset
struct __struct_18__ {
	__struct_17__ __0; // _dateTime
	int16_t __1; // _offsetMinutes
};
// System.Guid
struct __struct_19__ {
	int32_t __0; // _a
	int16_t __1; // _b
	int16_t __2; // _c
	uint8_t __3; // _d
	uint8_t __4; // _e
	uint8_t __5; // _f
	uint8_t __6; // _g
	uint8_t __7; // _h
	uint8_t __8; // _i
	uint8_t __9; // _j
	uint8_t __10; // _k
};
// System.TimeSpan
struct __struct_22__ {
	int64_t __0; // _ticks
};
// System.TimeZoneInfo/TransitionTime
struct __struct_23__ {
	__struct_17__ __0; // _timeOfDay
	uint8_t __1; // _month
	uint8_t __2; // _week
	uint8_t __3; // _day
	int32_t __4; // _dayOfWeek
	uint8_t __5; // _isFixedDateRule
};
// System.ValueTuple`5<System.Object,System.Object,System.Object,System.Object,System.Object>
struct __struct_25__ {
	uintptr_t __0; // Item1
	uintptr_t __1; // Item2
	uintptr_t __2; // Item3
	uintptr_t __3; // Item4
	uintptr_t __4; // Item5
};
// System.ValueTuple
union __struct_26__ {
	struct { char __fieldSize_offsetPadding[1];};
	struct {
	};
};
// Unity.Collections.NativeArray`1<System.Object>
struct __struct_27__ {
	uintptr_t __0; // m_Buffer
	int32_t __1; // m_Length
	int32_t __2; // m_AllocatorLabel
};
// UnityEngine.Vector3
struct __struct_46__ {
	float __0; // x
	float __1; // y
	float __2; // z
};
// UnityEngine.Bounds
struct __struct_30__ {
	__struct_46__ __0; // m_Center
	__struct_46__ __1; // m_Extents
};
// UnityEngine.Color
struct __struct_31__ {
	float __0; // r
	float __1; // g
	float __2; // b
	float __3; // a
};
// UnityEngine.Matrix4x4
struct __struct_35__ {
	float __0; // m00
	float __1; // m10
	float __2; // m20
	float __3; // m30
	float __4; // m01
	float __5; // m11
	float __6; // m21
	float __7; // m31
	float __8; // m02
	float __9; // m12
	float __10; // m22
	float __11; // m32
	float __12; // m03
	float __13; // m13
	float __14; // m23
	float __15; // m33
};
// UnityEngine.Playables.PlayableHandle
struct __struct_36__ {
	intptr_t __0; // m_Handle
	uint32_t __1; // m_Version
};
// UnityEngine.Rendering.LODParameters
struct __struct_42__ {
	int32_t __0; // m_IsOrthographic
	__struct_46__ __1; // m_CameraPosition
	float __2; // m_FieldOfView
	float __3; // m_OrthoSize
	int32_t __4; // m_CameraPixelHeight
};
// UnityEngine.Rendering.ShaderTagId
struct __struct_44__ {
	int32_t __0; // m_Id
};
// UnityEngine.Vector2
struct __struct_45__ {
	float __0; // x
	float __1; // y
};
// System.Threading.CancellationToken
struct __struct_55__ {
	uintptr_t __0; // _source
};
// System.Threading.Tasks.VoidTaskResult
union __struct_56__ {
	struct { char __fieldSize_offsetPadding[1];};
	struct {
	};
};
// System.ValueTuple`2<System.Int32,System.Int32>
struct __struct_57__ {
	int32_t __0; // Item1
	int32_t __1; // Item2
};
// System.ValueTuple`5<System.IntPtr,System.Int32,System.IntPtr,System.Int32,System.Byte>
struct __struct_59__ {
	intptr_t __0; // Item1
	int32_t __1; // Item2
	intptr_t __2; // Item3
	int32_t __3; // Item4
	uint8_t __4; // Item5
};
// System.Collections.Generic.Dictionary`2/ValueCollection/Enumerator<System.Int32,System.Object>
struct __struct_63__ {
	uintptr_t __0; // _dictionary
	int32_t __1; // _index
	int32_t __2; // _version
	uintptr_t __3; // _currentValue
};
// System.Collections.Generic.Dictionary`2/ValueCollection/Enumerator<System.Object,System.Int32>
struct __struct_64__ {
	uintptr_t __0; // _dictionary
	int32_t __1; // _index
	int32_t __2; // _version
	int32_t __3; // _currentValue
};
// System.Collections.Generic.Dictionary`2/ValueCollection/Enumerator<System.Object,System.Resources.ResourceLocator>
struct __struct_66__ {
	uintptr_t __0; // _dictionary
	int32_t __1; // _index
	int32_t __2; // _version
	__struct_4__ __3; // _currentValue
};
// System.Collections.Generic.List`1/Enumerator<UnityEngine.BeforeRenderHelper/OrderBlock>
struct __struct_71__ {
	uintptr_t __0; // _list
	int32_t __1; // _index
	int32_t __2; // _version
	__struct_6__ __3; // _current
};
// System.Collections.Generic.List`1/Enumerator<UnityEngine.UnitySynchronizationContext/WorkRequest>
struct __struct_72__ {
	uintptr_t __0; // _list
	int32_t __1; // _index
	int32_t __2; // _version
	__struct_9__ __3; // _current
};
// System.Nullable`1<System.Int32>
struct __struct_74__ {
	uint8_t __0; // hasValue
	int32_t __1; // value
};
// System.Nullable`1<System.Byte>
struct __struct_75__ {
	uint8_t __0; // hasValue
	uint8_t __1; // value
};
// System.Nullable`1<System.DateTime>
struct __struct_76__ {
	uint8_t __0; // hasValue
	__struct_17__ __1; // value
};
// System.Nullable`1<System.Decimal>
struct __struct_77__ {
	uint8_t __0; // hasValue
	__struct_16__ __1; // value
};
// System.Nullable`1<System.TimeSpan>
struct __struct_78__ {
	uint8_t __0; // hasValue
	__struct_22__ __1; // value
};
// System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1/ConfiguredTaskAwaiter<System.Byte>
struct __struct_88__ {
	uintptr_t __0; // m_task
	uint8_t __1; // m_continueOnCapturedContext
};
// UnityEngine.Profiling.Experimental.DebugScreenCapture
struct __struct_104__ {
	__struct_27__ __0; // <rawImageDataReference>k__BackingField
	int32_t __1; // <imageFormat>k__BackingField
	int32_t __2; // <width>k__BackingField
	int32_t __3; // <height>k__BackingField
};
// UnityEngine.Experimental.GlobalIllumination.LightDataGI
struct __struct_105__ {
	int32_t __0; // instanceID
	int32_t __1; // cookieID
	float __2; // cookieScale
	__struct_31__ __3; // color
	__struct_31__ __4; // indirectColor
	__struct_31__ __5; // orientation
	__struct_46__ __6; // position
	float __7; // range
	float __8; // coneAngle
	float __9; // innerConeAngle
	float __10; // shape0
	float __11; // shape1
	uint8_t __12; // type
	uint8_t __13; // mode
	uint8_t __14; // shadow
	uint8_t __15; // falloff
};
// UnityEngine.CullingGroupEvent
struct __struct_106__ {
	int32_t __0; // m_Index
	uint8_t __1; // m_PrevState
	uint8_t __2; // m_ThisState
};
// UnityEngine.Playables.FrameData
struct __struct_108__ {
	uint64_t __0; // m_FrameID
	double __1; // m_DeltaTime
	float __2; // m_Weight
	float __3; // m_EffectiveWeight
	double __4; // m_EffectiveParentDelay
	float __5; // m_EffectiveParentSpeed
	float __6; // m_EffectiveSpeed
	int32_t __7; // m_Flags
	__struct_36__ __8; // m_Output
};
// UnityEngine.RenderTextureDescriptor
struct __struct_110__ {
	int32_t __0; // <width>k__BackingField
	int32_t __1; // <height>k__BackingField
	int32_t __2; // <msaaSamples>k__BackingField
	int32_t __3; // <volumeDepth>k__BackingField
	int32_t __4; // <mipCount>k__BackingField
	int32_t __5; // _graphicsFormat
	int32_t __6; // <stencilFormat>k__BackingField
	int32_t __7; // <depthStencilFormat>k__BackingField
	int32_t __8; // <dimension>k__BackingField
	int32_t __9; // <shadowSamplingMode>k__BackingField
	int32_t __10; // <vrUsage>k__BackingField
	int32_t __11; // _flags
	int32_t __12; // <memoryless>k__BackingField
};
// System.Collections.Generic.KeyValuePair`2<System.Int32,UnityEngine.Vector2>
struct __struct_111__ {
	int32_t __0; // key
	__struct_45__ __1; // value
};
// UnityEngine.Rendering.BatchCullingContext
struct __struct_117__ {
	__struct_27__ __0; // cullingPlanes
	__struct_27__ __1; // batchVisibility
	__struct_27__ __2; // visibleIndices
	__struct_27__ __3; // visibleIndicesY
	__struct_42__ __4; // lodParameters
	__struct_35__ __5; // cullingMatrix
	float __6; // nearPlane
};
// UnityEngine.LightBakingOutput
struct __struct_120__ {
	int32_t __0; // probeOcclusionLightIndex
	int32_t __1; // occlusionMaskChannel
	int32_t __2; // lightmapBakeType
	int32_t __3; // mixedLightingMode
	uint8_t __4; // isBaked
};
// System.Numerics.Register
union __struct_125__ {
	#pragma pack(push, 1)
	struct {   uint8_t __0;}; // byte_0
	#pragma pack(pop)
	struct { uint8_t __0_forAlignmentOnly;}; // byte_0
	#pragma pack(push, 1)
	struct { char __1_offsetPadding[1];  uint8_t __1;}; // byte_1
	#pragma pack(pop)
	struct { uint8_t __1_forAlignmentOnly;}; // byte_1
	#pragma pack(push, 1)
	struct { char __2_offsetPadding[2];  uint8_t __2;}; // byte_2
	#pragma pack(pop)
	struct { uint8_t __2_forAlignmentOnly;}; // byte_2
	#pragma pack(push, 1)
	struct { char __3_offsetPadding[3];  uint8_t __3;}; // byte_3
	#pragma pack(pop)
	struct { uint8_t __3_forAlignmentOnly;}; // byte_3
	#pragma pack(push, 1)
	struct { char __4_offsetPadding[4];  uint8_t __4;}; // byte_4
	#pragma pack(pop)
	struct { uint8_t __4_forAlignmentOnly;}; // byte_4
	#pragma pack(push, 1)
	struct { char __5_offsetPadding[5];  uint8_t __5;}; // byte_5
	#pragma pack(pop)
	struct { uint8_t __5_forAlignmentOnly;}; // byte_5
	#pragma pack(push, 1)
	struct { char __6_offsetPadding[6];  uint8_t __6;}; // byte_6
	#pragma pack(pop)
	struct { uint8_t __6_forAlignmentOnly;}; // byte_6
	#pragma pack(push, 1)
	struct { char __7_offsetPadding[7];  uint8_t __7;}; // byte_7
	#pragma pack(pop)
	struct { uint8_t __7_forAlignmentOnly;}; // byte_7
	#pragma pack(push, 1)
	struct { char __8_offsetPadding[8];  uint8_t __8;}; // byte_8
	#pragma pack(pop)
	struct { uint8_t __8_forAlignmentOnly;}; // byte_8
	#pragma pack(push, 1)
	struct { char __9_offsetPadding[9];  uint8_t __9;}; // byte_9
	#pragma pack(pop)
	struct { uint8_t __9_forAlignmentOnly;}; // byte_9
	#pragma pack(push, 1)
	struct { char __10_offsetPadding[10];  uint8_t __10;}; // byte_10
	#pragma pack(pop)
	struct { uint8_t __10_forAlignmentOnly;}; // byte_10
	#pragma pack(push, 1)
	struct { char __11_offsetPadding[11];  uint8_t __11;}; // byte_11
	#pragma pack(pop)
	struct { uint8_t __11_forAlignmentOnly;}; // byte_11
	#pragma pack(push, 1)
	struct { char __12_offsetPadding[12];  uint8_t __12;}; // byte_12
	#pragma pack(pop)
	struct { uint8_t __12_forAlignmentOnly;}; // byte_12
	#pragma pack(push, 1)
	struct { char __13_offsetPadding[13];  uint8_t __13;}; // byte_13
	#pragma pack(pop)
	struct { uint8_t __13_forAlignmentOnly;}; // byte_13
	#pragma pack(push, 1)
	struct { char __14_offsetPadding[14];  uint8_t __14;}; // byte_14
	#pragma pack(pop)
	struct { uint8_t __14_forAlignmentOnly;}; // byte_14
	#pragma pack(push, 1)
	struct { char __15_offsetPadding[15];  uint8_t __15;}; // byte_15
	#pragma pack(pop)
	struct { uint8_t __15_forAlignmentOnly;}; // byte_15
	#pragma pack(push, 1)
	struct {   int8_t __16;}; // sbyte_0
	#pragma pack(pop)
	struct { int8_t __16_forAlignmentOnly;}; // sbyte_0
	#pragma pack(push, 1)
	struct { char __17_offsetPadding[1];  int8_t __17;}; // sbyte_1
	#pragma pack(pop)
	struct { int8_t __17_forAlignmentOnly;}; // sbyte_1
	#pragma pack(push, 1)
	struct { char __18_offsetPadding[2];  int8_t __18;}; // sbyte_2
	#pragma pack(pop)
	struct { int8_t __18_forAlignmentOnly;}; // sbyte_2
	#pragma pack(push, 1)
	struct { char __19_offsetPadding[3];  int8_t __19;}; // sbyte_3
	#pragma pack(pop)
	struct { int8_t __19_forAlignmentOnly;}; // sbyte_3
	#pragma pack(push, 1)
	struct { char __20_offsetPadding[4];  int8_t __20;}; // sbyte_4
	#pragma pack(pop)
	struct { int8_t __20_forAlignmentOnly;}; // sbyte_4
	#pragma pack(push, 1)
	struct { char __21_offsetPadding[5];  int8_t __21;}; // sbyte_5
	#pragma pack(pop)
	struct { int8_t __21_forAlignmentOnly;}; // sbyte_5
	#pragma pack(push, 1)
	struct { char __22_offsetPadding[6];  int8_t __22;}; // sbyte_6
	#pragma pack(pop)
	struct { int8_t __22_forAlignmentOnly;}; // sbyte_6
	#pragma pack(push, 1)
	struct { char __23_offsetPadding[7];  int8_t __23;}; // sbyte_7
	#pragma pack(pop)
	struct { int8_t __23_forAlignmentOnly;}; // sbyte_7
	#pragma pack(push, 1)
	struct { char __24_offsetPadding[8];  int8_t __24;}; // sbyte_8
	#pragma pack(pop)
	struct { int8_t __24_forAlignmentOnly;}; // sbyte_8
	#pragma pack(push, 1)
	struct { char __25_offsetPadding[9];  int8_t __25;}; // sbyte_9
	#pragma pack(pop)
	struct { int8_t __25_forAlignmentOnly;}; // sbyte_9
	#pragma pack(push, 1)
	struct { char __26_offsetPadding[10];  int8_t __26;}; // sbyte_10
	#pragma pack(pop)
	struct { int8_t __26_forAlignmentOnly;}; // sbyte_10
	#pragma pack(push, 1)
	struct { char __27_offsetPadding[11];  int8_t __27;}; // sbyte_11
	#pragma pack(pop)
	struct { int8_t __27_forAlignmentOnly;}; // sbyte_11
	#pragma pack(push, 1)
	struct { char __28_offsetPadding[12];  int8_t __28;}; // sbyte_12
	#pragma pack(pop)
	struct { int8_t __28_forAlignmentOnly;}; // sbyte_12
	#pragma pack(push, 1)
	struct { char __29_offsetPadding[13];  int8_t __29;}; // sbyte_13
	#pragma pack(pop)
	struct { int8_t __29_forAlignmentOnly;}; // sbyte_13
	#pragma pack(push, 1)
	struct { char __30_offsetPadding[14];  int8_t __30;}; // sbyte_14
	#pragma pack(pop)
	struct { int8_t __30_forAlignmentOnly;}; // sbyte_14
	#pragma pack(push, 1)
	struct { char __31_offsetPadding[15];  int8_t __31;}; // sbyte_15
	#pragma pack(pop)
	struct { int8_t __31_forAlignmentOnly;}; // sbyte_15
	#pragma pack(push, 1)
	struct {   uint16_t __32;}; // uint16_0
	#pragma pack(pop)
	struct { uint16_t __32_forAlignmentOnly;}; // uint16_0
	#pragma pack(push, 1)
	struct { char __33_offsetPadding[2];  uint16_t __33;}; // uint16_1
	#pragma pack(pop)
	struct { uint16_t __33_forAlignmentOnly;}; // uint16_1
	#pragma pack(push, 1)
	struct { char __34_offsetPadding[4];  uint16_t __34;}; // uint16_2
	#pragma pack(pop)
	struct { uint16_t __34_forAlignmentOnly;}; // uint16_2
	#pragma pack(push, 1)
	struct { char __35_offsetPadding[6];  uint16_t __35;}; // uint16_3
	#pragma pack(pop)
	struct { uint16_t __35_forAlignmentOnly;}; // uint16_3
	#pragma pack(push, 1)
	struct { char __36_offsetPadding[8];  uint16_t __36;}; // uint16_4
	#pragma pack(pop)
	struct { uint16_t __36_forAlignmentOnly;}; // uint16_4
	#pragma pack(push, 1)
	struct { char __37_offsetPadding[10];  uint16_t __37;}; // uint16_5
	#pragma pack(pop)
	struct { uint16_t __37_forAlignmentOnly;}; // uint16_5
	#pragma pack(push, 1)
	struct { char __38_offsetPadding[12];  uint16_t __38;}; // uint16_6
	#pragma pack(pop)
	struct { uint16_t __38_forAlignmentOnly;}; // uint16_6
	#pragma pack(push, 1)
	struct { char __39_offsetPadding[14];  uint16_t __39;}; // uint16_7
	#pragma pack(pop)
	struct { uint16_t __39_forAlignmentOnly;}; // uint16_7
	#pragma pack(push, 1)
	struct {   int16_t __40;}; // int16_0
	#pragma pack(pop)
	struct { int16_t __40_forAlignmentOnly;}; // int16_0
	#pragma pack(push, 1)
	struct { char __41_offsetPadding[2];  int16_t __41;}; // int16_1
	#pragma pack(pop)
	struct { int16_t __41_forAlignmentOnly;}; // int16_1
	#pragma pack(push, 1)
	struct { char __42_offsetPadding[4];  int16_t __42;}; // int16_2
	#pragma pack(pop)
	struct { int16_t __42_forAlignmentOnly;}; // int16_2
	#pragma pack(push, 1)
	struct { char __43_offsetPadding[6];  int16_t __43;}; // int16_3
	#pragma pack(pop)
	struct { int16_t __43_forAlignmentOnly;}; // int16_3
	#pragma pack(push, 1)
	struct { char __44_offsetPadding[8];  int16_t __44;}; // int16_4
	#pragma pack(pop)
	struct { int16_t __44_forAlignmentOnly;}; // int16_4
	#pragma pack(push, 1)
	struct { char __45_offsetPadding[10];  int16_t __45;}; // int16_5
	#pragma pack(pop)
	struct { int16_t __45_forAlignmentOnly;}; // int16_5
	#pragma pack(push, 1)
	struct { char __46_offsetPadding[12];  int16_t __46;}; // int16_6
	#pragma pack(pop)
	struct { int16_t __46_forAlignmentOnly;}; // int16_6
	#pragma pack(push, 1)
	struct { char __47_offsetPadding[14];  int16_t __47;}; // int16_7
	#pragma pack(pop)
	struct { int16_t __47_forAlignmentOnly;}; // int16_7
	#pragma pack(push, 1)
	struct {   uint32_t __48;}; // uint32_0
	#pragma pack(pop)
	struct { uint32_t __48_forAlignmentOnly;}; // uint32_0
	#pragma pack(push, 1)
	struct { char __49_offsetPadding[4];  uint32_t __49;}; // uint32_1
	#pragma pack(pop)
	struct { uint32_t __49_forAlignmentOnly;}; // uint32_1
	#pragma pack(push, 1)
	struct { char __50_offsetPadding[8];  uint32_t __50;}; // uint32_2
	#pragma pack(pop)
	struct { uint32_t __50_forAlignmentOnly;}; // uint32_2
	#pragma pack(push, 1)
	struct { char __51_offsetPadding[12];  uint32_t __51;}; // uint32_3
	#pragma pack(pop)
	struct { uint32_t __51_forAlignmentOnly;}; // uint32_3
	#pragma pack(push, 1)
	struct {   int32_t __52;}; // int32_0
	#pragma pack(pop)
	struct { int32_t __52_forAlignmentOnly;}; // int32_0
	#pragma pack(push, 1)
	struct { char __53_offsetPadding[4];  int32_t __53;}; // int32_1
	#pragma pack(pop)
	struct { int32_t __53_forAlignmentOnly;}; // int32_1
	#pragma pack(push, 1)
	struct { char __54_offsetPadding[8];  int32_t __54;}; // int32_2
	#pragma pack(pop)
	struct { int32_t __54_forAlignmentOnly;}; // int32_2
	#pragma pack(push, 1)
	struct { char __55_offsetPadding[12];  int32_t __55;}; // int32_3
	#pragma pack(pop)
	struct { int32_t __55_forAlignmentOnly;}; // int32_3
	#pragma pack(push, 1)
	struct {   uint64_t __56;}; // uint64_0
	#pragma pack(pop)
	struct { uint64_t __56_forAlignmentOnly;}; // uint64_0
	#pragma pack(push, 1)
	struct { char __57_offsetPadding[8];  uint64_t __57;}; // uint64_1
	#pragma pack(pop)
	struct { uint64_t __57_forAlignmentOnly;}; // uint64_1
	#pragma pack(push, 1)
	struct {   int64_t __58;}; // int64_0
	#pragma pack(pop)
	struct { int64_t __58_forAlignmentOnly;}; // int64_0
	#pragma pack(push, 1)
	struct { char __59_offsetPadding[8];  int64_t __59;}; // int64_1
	#pragma pack(pop)
	struct { int64_t __59_forAlignmentOnly;}; // int64_1
	#pragma pack(push, 1)
	struct {   float __60;}; // single_0
	#pragma pack(pop)
	struct { float __60_forAlignmentOnly;}; // single_0
	#pragma pack(push, 1)
	struct { char __61_offsetPadding[4];  float __61;}; // single_1
	#pragma pack(pop)
	struct { float __61_forAlignmentOnly;}; // single_1
	#pragma pack(push, 1)
	struct { char __62_offsetPadding[8];  float __62;}; // single_2
	#pragma pack(pop)
	struct { float __62_forAlignmentOnly;}; // single_2
	#pragma pack(push, 1)
	struct { char __63_offsetPadding[12];  float __63;}; // single_3
	#pragma pack(pop)
	struct { float __63_forAlignmentOnly;}; // single_3
	#pragma pack(push, 1)
	struct {   double __64;}; // double_0
	#pragma pack(pop)
	struct { double __64_forAlignmentOnly;}; // double_0
	#pragma pack(push, 1)
	struct { char __65_offsetPadding[8];  double __65;}; // double_1
	#pragma pack(pop)
	struct { double __65_forAlignmentOnly;}; // double_1
};
FullName2Signature hybridclr::interpreter::g_fullName2SignatureStub[] = {
	{"System.Span`1<u1>", "s0"},
	{"System.ReadOnlySpan`1<u2>", "s0"},
	{"System.Span`1<u2>", "s0"},
	{"System.Collections.Generic.KeyValuePair`2<u,u>", "s3"},
	{"System.Resources.ResourceLocator", "s4"},
	{"System.ValueTuple`2<u,u>", "s3"},
	{"System.Collections.Generic.KeyValuePair`2<i4,u>", "s6"},
	{"System.Collections.Generic.KeyValuePair`2<u,i4>", "s4"},
	{"System.Collections.Generic.KeyValuePair`2<u,System.Resources.ResourceLocator>", "s8"},
	{"System.Collections.Generic.KeyValuePair`2<System.ValueTuple`2<u,u>,u>", "s9"},
	{"System.Nullable`1<u>", "s10"},
	{"UnityEngine.BeforeRenderHelper/OrderBlock", "s6"},
	{"UnityEngine.UnitySynchronizationContext/WorkRequest", "s9"},
	{"System.Reflection.CustomAttributeNamedArgument", "s13"},
	{"System.Reflection.CustomAttributeTypedArgument", "s3"},
	{"System.ConsoleKeyInfo", "s15"},
	{"System.Decimal", "s16"},
	{"System.DateTime", "s17"},
	{"System.DateTimeOffset", "s18"},
	{"System.Guid", "s19"},
	{"System.Numerics.Vector`1<u>", "s125"},
	{"System.Threading.CancellationTokenRegistration", "s8"},
	{"System.TimeSpan", "s22"},
	{"System.TimeZoneInfo/TransitionTime", "s23"},
	{"System.ValueTuple`3<u,u,u>", "s9"},
	{"System.ValueTuple`5<u,u,u,u,u>", "s25"},
	{"System.ValueTuple", "s26"},
	{"Unity.Collections.NativeArray`1<u>", "s27"},
	{"UnityEngine.Audio.AudioClipPlayable", "s36"},
	{"UnityEngine.Audio.AudioMixerPlayable", "s36"},
	{"UnityEngine.Bounds", "s30"},
	{"UnityEngine.Color", "s31"},
	{"UnityEngine.Experimental.Playables.CameraPlayable", "s36"},
	{"UnityEngine.Experimental.Playables.MaterialEffectPlayable", "s36"},
	{"UnityEngine.Experimental.Playables.TextureMixerPlayable", "s36"},
	{"UnityEngine.Matrix4x4", "s35"},
	{"UnityEngine.Playables.PlayableHandle", "s36"},
	{"UnityEngine.Playables.PlayableOutputHandle", "s36"},
	{"UnityEngine.Playables.PlayableOutput", "s36"},
	{"UnityEngine.Playables.Playable", "s36"},
	{"UnityEngine.Quaternion", "s31"},
	{"UnityEngine.Rect", "s31"},
	{"UnityEngine.Rendering.LODParameters", "s42"},
	{"UnityEngine.Rendering.ScriptableRenderContext", "s123"},
	{"UnityEngine.Rendering.ShaderTagId", "s44"},
	{"UnityEngine.Vector2", "s45"},
	{"UnityEngine.Vector3", "s46"},
	{"UnityEngine.Vector4", "s31"},
	{"System.ReadOnlySpan`1<u>", "s0"},
	{"System.Numerics.Vector`1<u2>", "s125"},
	{"System.Numerics.Vector`1<u8>", "s125"},
	{"System.Span`1<i4>", "s0"},
	{"System.Span`1<u>", "s0"},
	{"System.Span`1<u4>", "s0"},
	{"System.Runtime.InteropServices.GCHandle", "s123"},
	{"System.Threading.CancellationToken", "s55"},
	{"System.Threading.Tasks.VoidTaskResult", "s56"},
	{"System.ValueTuple`2<i4,i4>", "s57"},
	{"System.ValueTuple`3<u,i4,i4>", "s27"},
	{"System.ValueTuple`5<i,i4,i,i4,u1>", "s59"},
	{"Unity.Collections.NativeArray`1<UnityEngine.Experimental.GlobalIllumination.LightDataGI>", "s27"},
	{"UnityEngine.SendMouseEvents/HitInfo", "s3"},
	{"System.Collections.DictionaryEntry", "s3"},
	{"System.Collections.Generic.Dictionary`2/ValueCollection/Enumerator<i4,u>", "s63"},
	{"System.Collections.Generic.Dictionary`2/ValueCollection/Enumerator<u,i4>", "s64"},
	{"System.Collections.Generic.Dictionary`2/ValueCollection/Enumerator<u,u>", "s63"},
	{"System.Collections.Generic.Dictionary`2/ValueCollection/Enumerator<u,System.Resources.ResourceLocator>", "s66"},
	{"System.Collections.Generic.Dictionary`2/ValueCollection/Enumerator<System.ValueTuple`2<u,u>,u>", "s63"},
	{"System.Collections.Generic.List`1/Enumerator<i4>", "s64"},
	{"System.Collections.Generic.List`1/Enumerator<u>", "s63"},
	{"System.Collections.Generic.List`1/Enumerator<UnityEngine.BeforeRenderHelper/OrderBlock>", "s71"},
	{"System.Collections.Generic.List`1/Enumerator<UnityEngine.UnitySynchronizationContext/WorkRequest>", "s72"},
	{"System.ReadOnlySpan`1<u1>", "s0"},
	{"System.Nullable`1<i4>", "s74"},
	{"System.Nullable`1<u1>", "s75"},
	{"System.Nullable`1<System.DateTime>", "s76"},
	{"System.Nullable`1<System.Decimal>", "s77"},
	{"System.Nullable`1<System.TimeSpan>", "s78"},
	{"System.Runtime.Serialization.StreamingContext", "s4"},
	{"System.ParameterizedStrings/FormatParam", "s6"},
	{"System.ReadOnlySpan`1<u4>", "s0"},
	{"System.ReadOnlySpan`1<i4>", "s0"},
	{"System.RuntimeFieldHandle", "s123"},
	{"System.RuntimeTypeHandle", "s123"},
	{"System.RuntimeMethodHandle", "s123"},
	{"System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<u1>", "s9"},
	{"System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<u>", "s9"},
	{"System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1/ConfiguredTaskAwaiter<u1>", "s88"},
	{"System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1/ConfiguredTaskAwaiter<i4>", "s88"},
	{"System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1/ConfiguredTaskAwaiter<u>", "s88"},
	{"System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1/ConfiguredTaskAwaiter<System.Threading.Tasks.VoidTaskResult>", "s88"},
	{"System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<u1>", "s88"},
	{"System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<i4>", "s88"},
	{"System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<u>", "s88"},
	{"System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<System.Threading.Tasks.VoidTaskResult>", "s88"},
	{"System.Runtime.CompilerServices.TaskAwaiter`1<u1>", "s55"},
	{"System.Runtime.CompilerServices.TaskAwaiter`1<i4>", "s55"},
	{"System.Runtime.CompilerServices.TaskAwaiter`1<u>", "s55"},
	{"System.Runtime.CompilerServices.TaskAwaiter`1<System.Threading.Tasks.VoidTaskResult>", "s55"},
	{"System.Runtime.Remoting.Messaging.LogicalCallContext/Reader", "s55"},
	{"System.Runtime.Serialization.SerializationEntry", "s9"},
	{"System.Threading.LockHolder", "s55"},
	{"System.Threading.SparselyPopulatedArrayAddInfo`1<u>", "s4"},
	{"UnityEngine.Profiling.Experimental.DebugScreenCapture", "s104"},
	{"UnityEngine.Experimental.GlobalIllumination.LightDataGI", "s105"},
	{"UnityEngine.CullingGroupEvent", "s106"},
	{"UnityEngine.SceneManagement.Scene", "s44"},
	{"UnityEngine.Playables.FrameData", "s108"},
	{"Unity.Collections.NativeArray`1<u1>", "s27"},
	{"UnityEngine.RenderTextureDescriptor", "s110"},
	{"System.Collections.Generic.KeyValuePair`2<i4,UnityEngine.Vector2>", "s111"},
	{"Unity.Collections.NativeArray`1/Enumerator<u>", "s64"},
	{"Unity.Collections.NativeArray`1/Enumerator<UnityEngine.Experimental.GlobalIllumination.LightDataGI>", "s64"},
	{"Unity.Collections.NativeArray`1<i4>", "s27"},
	{"Unity.Collections.NativeArray`1<UnityEngine.Plane>", "s27"},
	{"Unity.Collections.NativeArray`1<UnityEngine.Rendering.BatchVisibility>", "s27"},
	{"UnityEngine.Rendering.BatchCullingContext", "s117"},
	{"Unity.Jobs.JobHandle", "s0"},
	{"UnityEngine.Experimental.GlobalIllumination.LinearColor", "s31"},
	{"UnityEngine.LightBakingOutput", "s120"},
	{"UnityEngine.Playables.PlayableGraph", "s36"},
	{"UnityEngine.Ray", "s30"},
	{ nullptr, nullptr},
};

static void __M2N_i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_i1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_i1i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(intptr_t __arg0, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i1i2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(int16_t __arg0, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i1i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i1i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i1ii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i1ii4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(intptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i1iiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i1r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i1r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(double __arg0, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i1s16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(__struct_16__ __arg0, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i1u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i1u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(uint16_t __arg0, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i1u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(uint32_t __arg0, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i1u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(uint64_t __arg0, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i1ui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i1uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i1uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_i2i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(intptr_t __arg0, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i2i1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(int8_t __arg0, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i2i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i2i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i2ii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i2ii4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(intptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i2iiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i2r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i2r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(double __arg0, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i2s16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(__struct_16__ __arg0, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i2u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i2u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i2u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(uint16_t __arg0, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i2u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(uint32_t __arg0, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i2u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(uint64_t __arg0, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i2ui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i2uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i2uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int16_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_i4i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(intptr_t __arg0, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i4i2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(int16_t __arg0, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(int32_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(int32_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4i4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(int32_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4i4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(int32_t __arg0, uint8_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i4ii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4ii4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(intptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4iiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4iu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(intptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i4r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(double __arg0, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i4s0i4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(__struct_0__ __arg0, int32_t __arg1, int32_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i4s0s0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(__struct_0__ __arg0, __struct_0__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4s0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(__struct_0__ __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4s0u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(__struct_0__ __arg0, uint8_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4s0u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(__struct_0__ __arg0, uint16_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4s0u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(__struct_0__ __arg0, uint64_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4s16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(__struct_16__ __arg0, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i4s17s17(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(__struct_17__ __arg0, __struct_17__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i4u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uint16_t __arg0, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i4u2i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uint16_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4u2u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uint16_t __arg0, uint16_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uint32_t __arg0, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i4u4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uint32_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uint64_t __arg0, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i4ui(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4ui1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int8_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4ui2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int16_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4ui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4ui4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i4ui4i4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4ui4i4s6u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, __struct_6__ __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_6__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4ui4i4s9u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, __struct_9__ __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_9__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4ui4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i4ui4i4u8u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, uint64_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4ui4i4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, uintptr_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4ui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4ui4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uint8_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4ui4u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uint64_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4ui4ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i4ui4ui4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_i4ui8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4uii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, intptr_t __arg1, intptr_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4ur4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, float __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4ur8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, double __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us0s0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, __struct_0__ __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4us10(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_10__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_10__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us10s10(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_10__ __arg1, __struct_10__ __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_10__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_10__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4us13(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_13__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_13__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_16__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us17(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_17__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us18(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_18__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_18__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us19(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_19__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_19__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_22__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us22u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_22__ __arg1, uint8_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4us25(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_25__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_25__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us26(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_26__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_26__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us27(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_27__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_27__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us3(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_3__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_4__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us57(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_57__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_57__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us59(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_59__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_59__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us6(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_6__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_6__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us6i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_6__ __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_6__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i4us6s6(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_6__ __arg1, __struct_6__ __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_6__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_6__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4us9(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_9__ __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_9__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4us9i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_9__ __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_9__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i4us9s9(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, __struct_9__ __arg1, __struct_9__ __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_9__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_9__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4uu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4uu1i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4uu1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uint8_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4uu2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4uu2i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4uu2i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i4uu4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uint32_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4uu8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uint64_t __arg1, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i4uu8u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uint64_t __arg1, uint64_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4uui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4uui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i4uui4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4uui4i4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_i4uui4i4s6u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, __struct_6__ __arg4, uintptr_t __arg5, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<__struct_6__>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_i4uui4i4s9u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, __struct_9__ __arg4, uintptr_t __arg5, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<__struct_9__>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_i4uui4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4uui4i4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uint8_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4uui4i4u8u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uint64_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_i4uui4i4ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, int32_t __arg5, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_i4uui4i4ui4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, int32_t __arg5, int32_t __arg6, int32_t __arg7, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[6]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[7]), method);
}


static void __M2N_i4uui4i4ui4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, int32_t __arg5, uint8_t __arg6, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_i4uui4i4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_i4uui4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uint8_t __arg3, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i4uui4ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, int32_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4uui4ui4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, int32_t __arg4, uint8_t __arg5, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_i4uuii4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, intptr_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4uus10i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_10__ __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_10__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4uus3i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_3__ __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4uus4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_4__ __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4uus6i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_6__ __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_6__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4uus9i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_9__ __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_9__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i4uuu1i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i4uuu1i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4uuui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i4uuui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_i4uuui4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, const MethodInfo* method);
    *(int32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_i8i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(intptr_t __arg0, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i8i1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(int8_t __arg0, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i8i2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(int16_t __arg0, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i8i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i8i8i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(int64_t __arg0, int64_t __arg1, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i8ii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i8ii4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(intptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i8iiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i8r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i8r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(double __arg0, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i8s0i4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(__struct_0__ __arg0, int32_t __arg1, int32_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i8s16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(__struct_16__ __arg0, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i8u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i8u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i8u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uint16_t __arg0, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i8u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uint32_t __arg0, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i8u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uint64_t __arg0, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_i8ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i8ui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i8ui8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i8ui8i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i8ui8i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, int32_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_i8ui8i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, int64_t __arg2, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_i8uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_i8uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef int64_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(int64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_ii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(intptr_t __arg0, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_ii4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_ii4ii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(int32_t __arg0, intptr_t __arg1, intptr_t __arg2, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_ii8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_iii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_iii4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(intptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_iiiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_iiiu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uint8_t __arg2, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_iiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(intptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_iiuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(intptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_iiuu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(intptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_iiuuu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(intptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint8_t __arg3, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_is123(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(__struct_123__ __arg0, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_123__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_iu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_iu1u1uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(uint8_t __arg0, uint8_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_iui(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(uintptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_iui4i4i4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_iuii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(uintptr_t __arg0, intptr_t __arg1, intptr_t __arg2, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_iuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef intptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(intptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_r4i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(intptr_t __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4i1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(int8_t __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4i2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(int16_t __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4ii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_r4ii4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(intptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_r4iiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_r4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4r4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(float __arg0, float __arg1, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_r4r4r4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(float __arg0, float __arg1, float __arg2, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_r4r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(double __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4s16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(__struct_16__ __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(uint16_t __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(uint32_t __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(uint64_t __arg0, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r4ui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_r4ur4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(uintptr_t __arg0, float __arg1, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_r4ur4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(uintptr_t __arg0, float __arg1, float __arg2, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_r4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_r4uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef float (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(float*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_r8i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(intptr_t __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8i1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(int8_t __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8i2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(int16_t __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8ii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_r8ii4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(intptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_r8iiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_r8r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(double __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8r8r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(double __arg0, double __arg1, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_r8s16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(__struct_16__ __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(uint16_t __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(uint32_t __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(uint64_t __arg0, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_r8ui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_r8ur8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(uintptr_t __arg0, double __arg1, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_r8ur8r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(uintptr_t __arg0, double __arg1, double __arg2, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_r8uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_r8uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef double (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(double*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_s0s0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_0__ (*NativeMethod)(__struct_0__ __arg0, const MethodInfo* method);
    *(__struct_0__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_0__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_0__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s0ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_0__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(__struct_0__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s0ui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_0__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(__struct_0__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_s0us0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_0__ (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, const MethodInfo* method);
    *(__struct_0__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s0uus117(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_0__ (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_117__ __arg2, const MethodInfo* method);
    *(__struct_0__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_117__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_s105u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_105__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_105__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s105ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_105__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(__struct_105__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s10u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_10__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_10__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s110u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_110__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_110__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s111u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_111__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_111__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s120u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_120__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_120__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s123i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_123__ (*NativeMethod)(intptr_t __arg0, const MethodInfo* method);
    *(__struct_123__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s123u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_123__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_123__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s123ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_123__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(__struct_123__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s125(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_125__ (*NativeMethod)(const MethodInfo* method);
    *(__struct_125__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_s125s125(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_125__ (*NativeMethod)(__struct_125__ __arg0, const MethodInfo* method);
    *(__struct_125__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_125__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s125s125s125(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_125__ (*NativeMethod)(__struct_125__ __arg0, __struct_125__ __arg1, const MethodInfo* method);
    *(__struct_125__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_125__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_125__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s125u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_125__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_125__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s13ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_13__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(__struct_13__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s15(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_15__ (*NativeMethod)(const MethodInfo* method);
    *(__struct_15__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_s15u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_15__ (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(__struct_15__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s15uu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_15__ (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method);
    *(__struct_15__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s16i1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(int8_t __arg0, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s16i2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(int16_t __arg0, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s16i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s16i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s16r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s16r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(double __arg0, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s16s16i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(__struct_16__ __arg0, int32_t __arg1, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s16u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s16u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s16u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(uint16_t __arg0, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s16u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(uint32_t __arg0, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s16u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(uint64_t __arg0, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s16ui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_s16uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_16__ (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(__struct_16__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s17(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_17__ (*NativeMethod)(const MethodInfo* method);
    *(__struct_17__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_s17i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_17__ (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(__struct_17__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s17s17i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_17__ (*NativeMethod)(__struct_17__ __arg0, int32_t __arg1, const MethodInfo* method);
    *(__struct_17__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s17s17s22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_17__ (*NativeMethod)(__struct_17__ __arg0, __struct_22__ __arg1, const MethodInfo* method);
    *(__struct_17__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s17u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_17__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_17__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s17ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_17__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(__struct_17__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s17ui4i4i4i4i4i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_17__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, int32_t __arg6, int32_t __arg7, int32_t __arg8, const MethodInfo* method);
    *(__struct_17__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[6]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[7]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[8]), method);
}


static void __M2N_s17ui8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_17__ (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, const MethodInfo* method);
    *(__struct_17__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s17ur8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_17__ (*NativeMethod)(uintptr_t __arg0, double __arg1, const MethodInfo* method);
    *(__struct_17__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s17us22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_17__ (*NativeMethod)(uintptr_t __arg0, __struct_22__ __arg1, const MethodInfo* method);
    *(__struct_17__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s17uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_17__ (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(__struct_17__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s18i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_18__ (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(__struct_18__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s19(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_19__ (*NativeMethod)(const MethodInfo* method);
    *(__struct_19__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_s19s0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_19__ (*NativeMethod)(__struct_0__ __arg0, const MethodInfo* method);
    *(__struct_19__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s19u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_19__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_19__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_22__ (*NativeMethod)(const MethodInfo* method);
    *(__struct_22__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_s22i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_22__ (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(__struct_22__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s22r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_22__ (*NativeMethod)(double __arg0, const MethodInfo* method);
    *(__struct_22__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s22s17s17(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_22__ (*NativeMethod)(__struct_17__ __arg0, __struct_17__ __arg1, const MethodInfo* method);
    *(__struct_22__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s22s22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_22__ (*NativeMethod)(__struct_22__ __arg0, const MethodInfo* method);
    *(__struct_22__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s22s22s22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_22__ (*NativeMethod)(__struct_22__ __arg0, __struct_22__ __arg1, const MethodInfo* method);
    *(__struct_22__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s22u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_22__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_22__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s22us17(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_22__ (*NativeMethod)(uintptr_t __arg0, __struct_17__ __arg1, const MethodInfo* method);
    *(__struct_22__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s22us22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_22__ (*NativeMethod)(uintptr_t __arg0, __struct_22__ __arg1, const MethodInfo* method);
    *(__struct_22__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s22uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_22__ (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(__struct_22__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s23s17i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_23__ (*NativeMethod)(__struct_17__ __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(__struct_23__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_s23s17i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_23__ (*NativeMethod)(__struct_17__ __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(__struct_23__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_s23u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_23__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_23__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s27ui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_27__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(__struct_27__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_s30us46(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_30__ (*NativeMethod)(uintptr_t __arg0, __struct_46__ __arg1, const MethodInfo* method);
    *(__struct_30__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s30us46i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_30__ (*NativeMethod)(uintptr_t __arg0, __struct_46__ __arg1, int32_t __arg2, const MethodInfo* method);
    *(__struct_30__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_s31(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_31__ (*NativeMethod)(const MethodInfo* method);
    *(__struct_31__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_s31r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_31__ (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(__struct_31__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s31s31(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_31__ (*NativeMethod)(__struct_31__ __arg0, const MethodInfo* method);
    *(__struct_31__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s31s31r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_31__ (*NativeMethod)(__struct_31__ __arg0, float __arg1, const MethodInfo* method);
    *(__struct_31__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s31u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_31__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_31__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s31ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_31__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(__struct_31__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s35s46s31s46(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_35__ (*NativeMethod)(__struct_46__ __arg0, __struct_31__ __arg1, __struct_46__ __arg2, const MethodInfo* method);
    *(__struct_35__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_s36(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_36__ (*NativeMethod)(const MethodInfo* method);
    *(__struct_36__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_s36u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_36__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_36__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s36us36u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_36__ (*NativeMethod)(uintptr_t __arg0, __struct_36__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(__struct_36__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_36__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_s3u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_3__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_3__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s3ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_3__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(__struct_3__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s45(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_45__ (*NativeMethod)(const MethodInfo* method);
    *(__struct_45__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_s45s46(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_45__ (*NativeMethod)(__struct_46__ __arg0, const MethodInfo* method);
    *(__struct_45__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s45u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_45__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_45__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s46(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_46__ (*NativeMethod)(const MethodInfo* method);
    *(__struct_46__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_s46s45(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_46__ (*NativeMethod)(__struct_45__ __arg0, const MethodInfo* method);
    *(__struct_46__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_45__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s46s46(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_46__ (*NativeMethod)(__struct_46__ __arg0, const MethodInfo* method);
    *(__struct_46__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s46s46s46(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_46__ (*NativeMethod)(__struct_46__ __arg0, __struct_46__ __arg1, const MethodInfo* method);
    *(__struct_46__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s46u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_46__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_46__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_4__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_4__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_4__ (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(__struct_4__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_55__ (*NativeMethod)(const MethodInfo* method);
    *(__struct_55__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_s55u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_55__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_55__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s56u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_56__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_56__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s56uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_56__ (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(__struct_56__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s63u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_63__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_63__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s64u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_64__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_64__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s66u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_66__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_66__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s6i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_6__ (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(__struct_6__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s6u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_6__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_6__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s6ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_6__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(__struct_6__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s71u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_71__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_71__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s72u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_72__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_72__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s74u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_74__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_74__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s75u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_75__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_75__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s76u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_76__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_76__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s77u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_77__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_77__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s78u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_78__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_78__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s88u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_88__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_88__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s88uu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_88__ (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method);
    *(__struct_88__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_s8u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_8__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_8__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s8uuuu1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_8__ (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint8_t __arg3, uint8_t __arg4, const MethodInfo* method);
    *(__struct_8__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_s9(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_9__ (*NativeMethod)(const MethodInfo* method);
    *(__struct_9__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_s9u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_9__ (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(__struct_9__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_s9ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef __struct_9__ (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(__struct_9__*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_typedbyrefuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef Il2CppTypedRef (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(Il2CppTypedRef*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_u1i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(intptr_t __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1i1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(int8_t __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1i2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(int16_t __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(int32_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1i4s0us0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(int32_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, __struct_0__ __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_u1i4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(int32_t __arg0, uint8_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1i4uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(int32_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1i8s0us0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(int64_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, __struct_0__ __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_u1ii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1ii4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(intptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1iiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1iu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(intptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1r4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(float __arg0, float __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1r4s0us0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(float __arg0, __struct_0__ __arg1, uintptr_t __arg2, __struct_0__ __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_u1r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(double __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1r8s0us0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(double __arg0, __struct_0__ __arg1, uintptr_t __arg2, __struct_0__ __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_u1s0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_0__ __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1s0s0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_0__ __arg0, __struct_0__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s0s0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_0__ __arg0, __struct_0__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1s0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_0__ __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s0u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_0__ __arg0, uint32_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s123s123(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_123__ __arg0, __struct_123__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_123__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_123__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s125s125(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_125__ __arg0, __struct_125__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_125__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_125__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s13s13(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_13__ __arg0, __struct_13__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_13__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_13__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_16__ __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1s16s0us0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_16__ __arg0, __struct_0__ __arg1, uintptr_t __arg2, __struct_0__ __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_u1s16s16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_16__ __arg0, __struct_16__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s17s17(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_17__ __arg0, __struct_17__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s19s19(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_19__ __arg0, __struct_19__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_19__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_19__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s22s22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_22__ __arg0, __struct_22__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s23s23(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_23__ __arg0, __struct_23__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_23__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_23__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s3(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_3__ __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1s36s36(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_36__ __arg0, __struct_36__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_36__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_36__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s3s3(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_3__ __arg0, __struct_3__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s45s45(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_45__ __arg0, __struct_45__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_45__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_45__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s46s46(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_46__ __arg0, __struct_46__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1s55s55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(__struct_55__ __arg0, __struct_55__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uint16_t __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1u2u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uint16_t __arg0, uint16_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uint32_t __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1u4s0us0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uint32_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, __struct_0__ __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_u1u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uint64_t __arg0, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u1u8s0us0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uint64_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, __struct_0__ __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_u1u8u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uint64_t __arg0, uint64_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1ui(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1ui1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, int8_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1ui2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, int16_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1ui4i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, intptr_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1ui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1ui4s55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, __struct_55__ __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1ui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1ui4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uint8_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1ui4uu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uint8_t __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1ui8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1uii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, intptr_t __arg1, intptr_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1ur4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, float __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1ur8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, double __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us0i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, int32_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1us0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1us0us0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, __struct_0__ __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1us0us0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, __struct_0__ __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_u1us10s10(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_10__ __arg1, __struct_10__ __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_10__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_10__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1us123(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_123__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_123__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us125(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_125__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_125__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us13(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_13__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_13__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us15(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_15__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_15__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_16__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us17(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_17__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us18(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_18__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_18__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us19(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_19__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_19__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us19i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_19__ __arg1, int32_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_19__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1us19u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_19__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_19__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1us19ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_19__ __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_19__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1us22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_22__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us22s22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_22__ __arg1, __struct_22__ __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1us23(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_23__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_23__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us25(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_25__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_25__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us26(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_26__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_26__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us27(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_27__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_27__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us3(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_3__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us30(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_30__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_30__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us31(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_31__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us35(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_35__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_35__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us36(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_36__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_36__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us3s3(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_3__ __arg1, __struct_3__ __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1us3u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_3__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1us3uu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_3__ __arg1, uintptr_t __arg2, uint8_t __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1us4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_4__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us42(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_42__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_42__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us44(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_44__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_44__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us45(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_45__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_45__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us46(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_46__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us4s4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_4__ __arg1, __struct_4__ __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1us55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_55__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us56(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_56__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_56__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us57(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_57__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_57__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us59(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_59__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_59__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us6(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_6__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_6__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us6s6(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_6__ __arg1, __struct_6__ __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_6__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_6__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1us8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_8__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_8__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us9(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_9__ __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_9__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1us9s9(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, __struct_9__ __arg1, __struct_9__ __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_9__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_9__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1uu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1uu1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uint8_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1uu2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1uu2i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1uu2u2i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, uint16_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1uu4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uint32_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1uu4i4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uint32_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_u1uu8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uint64_t __arg1, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u1uui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1uui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1uui4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uint8_t __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1uui4uu1u1u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, uint8_t __arg4, uint8_t __arg5, uintptr_t __arg6, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_u1uus4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_4__ __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1uus4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_4__ __arg2, uint8_t __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1uuu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1uuu8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uint64_t __arg2, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u1uuui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1uuui4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_u1uuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1uuuu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint8_t __arg3, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_u1uuuu1u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint8_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint8_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uint8_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_u2i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(intptr_t __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2i1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(int8_t __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2i2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(int16_t __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2ii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u2ii4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(intptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u2iiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u2r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(double __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2s16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(__struct_16__ __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(uint16_t __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(uint32_t __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(uint64_t __arg0, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u2ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u2ui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u2uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u2uu2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u2uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint16_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uint16_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_u4i1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(int8_t __arg0, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u4i2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(int16_t __arg0, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u4i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u4r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(double __arg0, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u4s16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(__struct_16__ __arg0, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u4u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(uint16_t __arg0, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u4u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(uint32_t __arg0, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u4u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(uint64_t __arg0, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u4ui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint32_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(uint32_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_u8i1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(int8_t __arg0, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u8i2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(int16_t __arg0, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u8i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u8i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(int64_t __arg0, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u8r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u8r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(double __arg0, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u8s16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(__struct_16__ __arg0, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u8u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u8u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u8u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(uint16_t __arg0, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u8u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(uint32_t __arg0, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_u8ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_u8ui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_u8uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uint64_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(uint64_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_ui(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(intptr_t __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_ui4s0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(int32_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_ui4s27u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(int32_t __arg0, __struct_27__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_27__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_ui4s55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(int32_t __arg0, __struct_55__ __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_ui4s59u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(int32_t __arg0, __struct_59__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_59__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_ui4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(int32_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_ui8s0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(int64_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uiiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_ur4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(float __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_ur4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(float __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_ur8uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(double __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_us0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_0__ __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_us0s0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_0__ __arg0, __struct_0__ __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_us10(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_10__ __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_10__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_us123(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_123__ __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_123__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_us123s123(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_123__ __arg0, __struct_123__ __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_123__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_123__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_us16s0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_16__ __arg0, __struct_0__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_us17s17s22s23s23(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_17__ __arg0, __struct_17__ __arg1, __struct_22__ __arg2, __struct_23__ __arg3, __struct_23__ __arg4, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_23__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<__struct_23__>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_us55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_55__ __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_us55s55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_55__ __arg0, __struct_55__ __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_us74(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_74__ __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_74__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_us75(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_75__ __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_75__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_us76(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_76__ __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_76__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_us77(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_77__ __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_77__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_us78(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(__struct_78__ __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_78__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_uu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_uu2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uint16_t __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_uu4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uint32_t __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_uu4s0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uint32_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uu8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uint64_t __arg0, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_uu8s0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uint64_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uui(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uui1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int8_t __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uui2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int16_t __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uui4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_uui4s55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, __struct_55__ __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uui4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uint8_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uui4u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uint16_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uui4ui4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, int32_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_uui4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_uui4uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_uui4uuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_uui4uuuuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, uintptr_t __arg6, uintptr_t __arg7, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[6]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[7]), method);
}


static void __M2N_uui8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uui8i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, int64_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uui8i8i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, int64_t __arg2, int64_t __arg3, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_uus0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uus19(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, __struct_19__ __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_19__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uus22uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, __struct_22__ __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_uus22uuuuu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, __struct_22__ __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, uint8_t __arg6, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_uus3(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, __struct_3__ __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uus4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, __struct_4__ __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uuu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uuu1i4i4i4ui4i4i4i4u1u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, uintptr_t __arg5, int32_t __arg6, int32_t __arg7, int32_t __arg8, int32_t __arg9, uint8_t __arg10, uintptr_t __arg11, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[6]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[7]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[8]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[9]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[10]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[11]), method);
}


static void __M2N_uuu1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uint8_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uuu2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uuu2i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uuu2u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, uint16_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uuu4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uint32_t __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uuu8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uint64_t __arg1, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_uuui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uuui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_uuui4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_uuui4i4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_uuui4ui4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, int32_t __arg4, uintptr_t __arg5, uintptr_t __arg6, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_uuui4uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_uuui4uuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, uintptr_t __arg6, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_uuui4uuuuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, uintptr_t __arg6, uintptr_t __arg7, uintptr_t __arg8, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[6]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[7]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[8]), method);
}


static void __M2N_uuus4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_4__ __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_uuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uuuu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_uuuu1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, uint8_t __arg3, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_uuuui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_uuuus22u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, __struct_22__ __arg3, uint8_t __arg4, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_uuuus4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, __struct_4__ __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_uuuus55i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, __struct_55__ __arg3, int32_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_uuuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_uuuuu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint8_t __arg3, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_uuuuu1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint8_t __arg3, uint8_t __arg4, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_uuuuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef uintptr_t (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    *(uintptr_t*)ret = ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_v(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(method);
}


static void __M2N_vi(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_vi4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(int32_t __arg0, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_vi4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(int32_t __arg0, int32_t __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vi4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(int32_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vi4i4i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(int32_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vi4i4uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(int32_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vi4ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(int32_t __arg0, uintptr_t __arg1, int32_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vi4uui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(int32_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vi4uuu8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(int32_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint64_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vii4i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, int32_t __arg1, intptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vii4i1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, int32_t __arg1, int8_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vii4i2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, int32_t __arg1, int16_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vii4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vii4i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, int32_t __arg1, int64_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vii4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, int32_t __arg1, float __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vii4r8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, int32_t __arg1, double __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vii4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, int32_t __arg1, uint8_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vii4u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, int32_t __arg1, uint16_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_viii(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, intptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_viii1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, int8_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_viii2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, int16_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_viii4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, int32_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_viii8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, int64_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_viir4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, float __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_viir8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, double __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_viiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_viiu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uint8_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_viiu2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, intptr_t __arg1, uint16_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_viui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_viuu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(intptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vs22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(__struct_22__ __arg0, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_vs31uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(__struct_31__ __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vs35(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(__struct_35__ __arg0, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_35__>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_vs46s46(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(__struct_46__ __arg0, __struct_46__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vs46s46s31(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(__struct_46__ __arg0, __struct_46__ __arg1, __struct_31__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vs46s46s31r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(__struct_46__ __arg0, __struct_46__ __arg1, __struct_31__ __arg2, float __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vs46s46s31r4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(__struct_46__ __arg0, __struct_46__ __arg1, __struct_31__ __arg2, float __arg3, uint8_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_46__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vtypedbyrefu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(Il2CppTypedRef __arg0, uintptr_t __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<Il2CppTypedRef>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_vu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uint8_t __arg0, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), method);
}


static void __M2N_vu1u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uint8_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vu1uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uint8_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vu1uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uint8_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vui(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, intptr_t __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vui2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int16_t __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vui4i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vui4i4i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vui4i4i4i4i4i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, intptr_t __arg6, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_vui4i4i4i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, int32_t __arg6, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_vui4i4i4i4i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, int32_t __arg6, int32_t __arg7, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[6]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[7]), method);
}


static void __M2N_vui4i4i4i4i4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, uint8_t __arg6, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_vui4i4i4i4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, uint8_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vui4i4i4i4u1i(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, uint8_t __arg5, intptr_t __arg6, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_vui4i4i4i4u1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, uint8_t __arg5, uint8_t __arg6, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_vui4i4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vui4i4i4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, uint8_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vui4i4i4u1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, uint8_t __arg4, uint8_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vui4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vui4i4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, uint8_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vui4i4u1u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, uint8_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vui4i4uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vui4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, float __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui4r4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, float __arg2, uintptr_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vui4s0i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, __struct_0__ __arg2, int32_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vui4s105(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, __struct_105__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_105__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui4s13(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, __struct_13__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_13__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui4s3(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, __struct_3__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui4s45(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, __struct_45__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_45__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui4s6(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, __struct_6__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_6__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui4s9(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, __struct_9__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_9__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uint8_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui4u2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uint16_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui4ui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vui4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vui4uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vui4uuuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, uintptr_t __arg6, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_vui8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vui8i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, int32_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui8s22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, __struct_22__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vui8ui8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, uintptr_t __arg2, int64_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vui8ui8i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, uintptr_t __arg2, int64_t __arg3, int64_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vui8uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, int64_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuii4ii4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, intptr_t __arg1, int32_t __arg2, intptr_t __arg3, int32_t __arg4, uint8_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vuiu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, intptr_t __arg1, uint8_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vur4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, float __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vur4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, float __arg1, float __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vur4r4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, float __arg1, float __arg2, float __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vur4r4r4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, float __arg1, float __arg2, float __arg3, float __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vur4r4r4r4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, float __arg1, float __arg2, float __arg3, float __arg4, uintptr_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vur8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, double __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<double>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus0i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, int64_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vus0s27(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, __struct_27__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_27__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vus0s59(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, __struct_59__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_59__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vus0u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vus106(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_106__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_106__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus110(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_110__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_110__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus123(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_123__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_123__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus123u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_123__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_123__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vus123uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_123__ __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_123__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vus13(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_13__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_13__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus15(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_15__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_15__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus16(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_16__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_16__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus17(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_17__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus17s17s22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_17__ __arg1, __struct_17__ __arg2, __struct_22__ __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vus19(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_19__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_19__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus19u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_19__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_19__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vus19ui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_19__ __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_19__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vus22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_22__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus27(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_27__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_27__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus3(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_3__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus31(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_31__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus31s31s31s31(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_31__ __arg1, __struct_31__ __arg2, __struct_31__ __arg3, __struct_31__ __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vus31uu1u1u1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_31__ __arg1, uintptr_t __arg2, uint8_t __arg3, uint8_t __arg4, uint8_t __arg5, uint8_t __arg6, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_31__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_vus36(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_36__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_36__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus36s108(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_36__ __arg1, __struct_108__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_36__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_108__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vus36s108u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_36__ __arg1, __struct_108__ __arg2, uintptr_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_36__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_108__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vus36uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_36__ __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_36__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vus3u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_3__ __arg1, uintptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vus4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_4__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus44(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_44__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_44__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus44i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_44__ __arg1, int32_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_44__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vus44s44(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_44__ __arg1, __struct_44__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_44__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_44__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vus45s45(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_45__ __arg1, __struct_45__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_45__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_45__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vus55i4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_55__ __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vus56(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_56__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_56__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus6(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_6__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_6__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_8__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_8__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vus9(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, __struct_9__ __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<__struct_9__>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vutypedbyrefu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, Il2CppTypedRef __arg1, uintptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<Il2CppTypedRef>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vuu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vuu1i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, int32_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuu1i4i4s0(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, int32_t __arg2, int32_t __arg3, __struct_0__ __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<__struct_0__>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuu1i4i4s55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, int32_t __arg2, int32_t __arg3, __struct_55__ __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuu1i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, int32_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuu1s56i4s55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, __struct_56__ __arg2, int32_t __arg3, __struct_55__ __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_56__>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuu1u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuu1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uint8_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuu1u1i4s55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uint8_t __arg2, int32_t __arg3, __struct_55__ __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuu1u1i4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uint8_t __arg2, int32_t __arg3, uint8_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuu1u1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uint8_t __arg2, uint8_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuu1u1u4u4u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uint8_t __arg2, uint32_t __arg3, uint32_t __arg4, uint32_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vuu1u1uuuui4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uint8_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, uintptr_t __arg6, int32_t __arg7, int32_t __arg8, uintptr_t __arg9, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[6]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[7]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[8]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[9]), method);
}


static void __M2N_vuu1ui4s55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uintptr_t __arg2, int32_t __arg3, __struct_55__ __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuu1uu1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uintptr_t __arg2, uint8_t __arg3, uint8_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuu1uuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint8_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuu2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vuu2i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, int32_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuu2i4i4i4i4i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, int32_t __arg6, int32_t __arg7, int32_t __arg8, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[6]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[7]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[8]), method);
}


static void __M2N_vuu2i4u1u1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint16_t __arg1, int32_t __arg2, uint8_t __arg3, uint8_t __arg4, uint8_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint16_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vuu4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint32_t __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vuu4i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuu4u4u4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint32_t __arg1, uint32_t __arg2, uint32_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuu8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint64_t __arg1, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[1]), method);
}


static void __M2N_vuu8u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uint64_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuui(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, intptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuui2(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int16_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int16_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuui4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuui4i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vuui4i4i4i4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, int32_t __arg6, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_vuui4i4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vuui4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuui4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uint8_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuui8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int64_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuui8i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int64_t __arg2, int64_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuui8i8i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int64_t __arg2, int64_t __arg3, int32_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuui8i8i8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int64_t __arg2, int64_t __arg3, int64_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuui8ui8uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, int64_t __arg2, uintptr_t __arg3, int64_t __arg4, uintptr_t __arg5, uintptr_t __arg6, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int64_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_vuuiu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, intptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<intptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuur4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, float __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuur4r4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, float __arg2, float __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuus17(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_17__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_17__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuus27(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_27__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_27__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuus3(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_3__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_3__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuus4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_4__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuus55(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, __struct_55__ __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuuu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuuu1i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, int32_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuuu1s104(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, __struct_104__ __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_104__>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuuu1u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, uint8_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuuu4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uint32_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint32_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuuu8(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uint64_t __arg2, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uint64_t>(localVarBase+argVarIndexs[2]), method);
}


static void __M2N_vuuui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuuui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuuui4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, int32_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vuuui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuuui4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, uint8_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuuui4uu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vuuur4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, float __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<float>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuuus22s22(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, __struct_22__ __arg3, __struct_22__ __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<__struct_22__>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuuus4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, __struct_4__ __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_4__>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuuus55i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, __struct_55__ __arg3, int32_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuuuu1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint8_t __arg3, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), method);
}


static void __M2N_vuuuu1i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint8_t __arg3, int32_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuuuu1i4u1(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint8_t __arg3, int32_t __arg4, uint8_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uint8_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vuuuui4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, int32_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuuuui4i4(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, int32_t __arg4, int32_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), method);
}


static void __M2N_vuuuus55i4i4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, __struct_55__ __arg4, int32_t __arg5, int32_t __arg6, uintptr_t __arg7, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<__struct_55__>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[6]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[7]), method);
}


static void __M2N_vuuuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), method);
}


static void __M2N_vuuuuui4u(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, int32_t __arg5, uintptr_t __arg6, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<int32_t>(localVarBase+argVarIndexs[5]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[6]), method);
}


static void __M2N_vuuuuuu(const MethodInfo* method, uint16_t* argVarIndexs, StackObject* localVarBase, void* ret)
{
    typedef void (*NativeMethod)(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method);
    ((NativeMethod)(method->methodPointerCallByInterp))(M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[0]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[1]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[2]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[3]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[4]), M2NFromValueOrAddress<uintptr_t>(localVarBase+argVarIndexs[5]), method);
}


Managed2NativeMethodInfo hybridclr::interpreter::g_managed2nativeStub[] = 
{

	{"i", __M2N_i},
	{"i1", __M2N_i1},
	{"i1i", __M2N_i1i},
	{"i1i2", __M2N_i1i2},
	{"i1i4", __M2N_i1i4},
	{"i1i8", __M2N_i1i8},
	{"i1ii", __M2N_i1ii},
	{"i1ii4", __M2N_i1ii4},
	{"i1iiu", __M2N_i1iiu},
	{"i1r4", __M2N_i1r4},
	{"i1r8", __M2N_i1r8},
	{"i1s16", __M2N_i1s16},
	{"i1u", __M2N_i1u},
	{"i1u1", __M2N_i1u1},
	{"i1u2", __M2N_i1u2},
	{"i1u4", __M2N_i1u4},
	{"i1u8", __M2N_i1u8},
	{"i1ui4u", __M2N_i1ui4u},
	{"i1uu", __M2N_i1uu},
	{"i1uuu", __M2N_i1uuu},
	{"i2", __M2N_i2},
	{"i2i", __M2N_i2i},
	{"i2i1", __M2N_i2i1},
	{"i2i4", __M2N_i2i4},
	{"i2i8", __M2N_i2i8},
	{"i2ii", __M2N_i2ii},
	{"i2ii4", __M2N_i2ii4},
	{"i2iiu", __M2N_i2iiu},
	{"i2r4", __M2N_i2r4},
	{"i2r8", __M2N_i2r8},
	{"i2s16", __M2N_i2s16},
	{"i2u", __M2N_i2u},
	{"i2u1", __M2N_i2u1},
	{"i2u2", __M2N_i2u2},
	{"i2u4", __M2N_i2u4},
	{"i2u8", __M2N_i2u8},
	{"i2ui4u", __M2N_i2ui4u},
	{"i2uu", __M2N_i2uu},
	{"i2uuu", __M2N_i2uuu},
	{"i4", __M2N_i4},
	{"i4i", __M2N_i4i},
	{"i4i2", __M2N_i4i2},
	{"i4i4", __M2N_i4i4},
	{"i4i4i4", __M2N_i4i4i4},
	{"i4i4i4i4", __M2N_i4i4i4i4},
	{"i4i4i4u", __M2N_i4i4i4u},
	{"i4i4u1", __M2N_i4i4u1},
	{"i4i8", __M2N_i4i8},
	{"i4ii", __M2N_i4ii},
	{"i4ii4", __M2N_i4ii4},
	{"i4iiu", __M2N_i4iiu},
	{"i4iu", __M2N_i4iu},
	{"i4r4", __M2N_i4r4},
	{"i4r8", __M2N_i4r8},
	{"i4s0i4i4u", __M2N_i4s0i4i4u},
	{"i4s0s0", __M2N_i4s0s0},
	{"i4s0u", __M2N_i4s0u},
	{"i4s0u1", __M2N_i4s0u1},
	{"i4s0u2", __M2N_i4s0u2},
	{"i4s0u8", __M2N_i4s0u8},
	{"i4s16", __M2N_i4s16},
	{"i4s17s17", __M2N_i4s17s17},
	{"i4u", __M2N_i4u},
	{"i4u1", __M2N_i4u1},
	{"i4u2", __M2N_i4u2},
	{"i4u2i4", __M2N_i4u2i4},
	{"i4u2u2", __M2N_i4u2u2},
	{"i4u4", __M2N_i4u4},
	{"i4u4i4", __M2N_i4u4i4},
	{"i4u8", __M2N_i4u8},
	{"i4ui", __M2N_i4ui},
	{"i4ui1", __M2N_i4ui1},
	{"i4ui2", __M2N_i4ui2},
	{"i4ui4", __M2N_i4ui4},
	{"i4ui4i4", __M2N_i4ui4i4},
	{"i4ui4i4i4", __M2N_i4ui4i4i4},
	{"i4ui4i4i4u", __M2N_i4ui4i4i4u},
	{"i4ui4i4s6u", __M2N_i4ui4i4s6u},
	{"i4ui4i4s9u", __M2N_i4ui4i4s9u},
	{"i4ui4i4u", __M2N_i4ui4i4u},
	{"i4ui4i4u8u", __M2N_i4ui4i4u8u},
	{"i4ui4i4uu", __M2N_i4ui4i4uu},
	{"i4ui4u", __M2N_i4ui4u},
	{"i4ui4u1", __M2N_i4ui4u1},
	{"i4ui4u8", __M2N_i4ui4u8},
	{"i4ui4ui4", __M2N_i4ui4ui4},
	{"i4ui4ui4i4i4", __M2N_i4ui4ui4i4i4},
	{"i4ui8", __M2N_i4ui8},
	{"i4uii", __M2N_i4uii},
	{"i4ur4", __M2N_i4ur4},
	{"i4ur8", __M2N_i4ur8},
	{"i4us0", __M2N_i4us0},
	{"i4us0s0", __M2N_i4us0s0},
	{"i4us10", __M2N_i4us10},
	{"i4us10s10", __M2N_i4us10s10},
	{"i4us13", __M2N_i4us13},
	{"i4us16", __M2N_i4us16},
	{"i4us17", __M2N_i4us17},
	{"i4us18", __M2N_i4us18},
	{"i4us19", __M2N_i4us19},
	{"i4us22", __M2N_i4us22},
	{"i4us22u1", __M2N_i4us22u1},
	{"i4us25", __M2N_i4us25},
	{"i4us26", __M2N_i4us26},
	{"i4us27", __M2N_i4us27},
	{"i4us3", __M2N_i4us3},
	{"i4us4", __M2N_i4us4},
	{"i4us57", __M2N_i4us57},
	{"i4us59", __M2N_i4us59},
	{"i4us6", __M2N_i4us6},
	{"i4us6i4i4", __M2N_i4us6i4i4},
	{"i4us6s6", __M2N_i4us6s6},
	{"i4us9", __M2N_i4us9},
	{"i4us9i4i4", __M2N_i4us9i4i4},
	{"i4us9s9", __M2N_i4us9s9},
	{"i4uu", __M2N_i4uu},
	{"i4uu1", __M2N_i4uu1},
	{"i4uu1i4", __M2N_i4uu1i4},
	{"i4uu1u1", __M2N_i4uu1u1},
	{"i4uu2", __M2N_i4uu2},
	{"i4uu2i4", __M2N_i4uu2i4},
	{"i4uu2i4i4", __M2N_i4uu2i4i4},
	{"i4uu4", __M2N_i4uu4},
	{"i4uu8", __M2N_i4uu8},
	{"i4uu8u8", __M2N_i4uu8u8},
	{"i4uui4", __M2N_i4uui4},
	{"i4uui4i4", __M2N_i4uui4i4},
	{"i4uui4i4i4", __M2N_i4uui4i4i4},
	{"i4uui4i4i4u", __M2N_i4uui4i4i4u},
	{"i4uui4i4s6u", __M2N_i4uui4i4s6u},
	{"i4uui4i4s9u", __M2N_i4uui4i4s9u},
	{"i4uui4i4u", __M2N_i4uui4i4u},
	{"i4uui4i4u1", __M2N_i4uui4i4u1},
	{"i4uui4i4u8u", __M2N_i4uui4i4u8u},
	{"i4uui4i4ui4", __M2N_i4uui4i4ui4},
	{"i4uui4i4ui4i4i4", __M2N_i4uui4i4ui4i4i4},
	{"i4uui4i4ui4u1", __M2N_i4uui4i4ui4u1},
	{"i4uui4i4uu", __M2N_i4uui4i4uu},
	{"i4uui4u1", __M2N_i4uui4u1},
	{"i4uui4ui4", __M2N_i4uui4ui4},
	{"i4uui4ui4u1", __M2N_i4uui4ui4u1},
	{"i4uuii4i4", __M2N_i4uuii4i4},
	{"i4uus10i4i4", __M2N_i4uus10i4i4},
	{"i4uus3i4i4", __M2N_i4uus3i4i4},
	{"i4uus4i4i4", __M2N_i4uus4i4i4},
	{"i4uus6i4i4", __M2N_i4uus6i4i4},
	{"i4uus9i4i4", __M2N_i4uus9i4i4},
	{"i4uuu", __M2N_i4uuu},
	{"i4uuu1i4", __M2N_i4uuu1i4},
	{"i4uuu1i4i4", __M2N_i4uuu1i4i4},
	{"i4uuui4", __M2N_i4uuui4},
	{"i4uuui4i4", __M2N_i4uuui4i4},
	{"i4uuui4i4i4", __M2N_i4uuui4i4i4},
	{"i8", __M2N_i8},
	{"i8i", __M2N_i8i},
	{"i8i1", __M2N_i8i1},
	{"i8i2", __M2N_i8i2},
	{"i8i4", __M2N_i8i4},
	{"i8i8i8", __M2N_i8i8i8},
	{"i8ii", __M2N_i8ii},
	{"i8ii4", __M2N_i8ii4},
	{"i8iiu", __M2N_i8iiu},
	{"i8r4", __M2N_i8r4},
	{"i8r8", __M2N_i8r8},
	{"i8s0i4i4u", __M2N_i8s0i4i4u},
	{"i8s16", __M2N_i8s16},
	{"i8u", __M2N_i8u},
	{"i8u1", __M2N_i8u1},
	{"i8u2", __M2N_i8u2},
	{"i8u4", __M2N_i8u4},
	{"i8u8", __M2N_i8u8},
	{"i8ui4", __M2N_i8ui4},
	{"i8ui4u", __M2N_i8ui4u},
	{"i8ui8", __M2N_i8ui8},
	{"i8ui8i4", __M2N_i8ui8i4},
	{"i8ui8i4u", __M2N_i8ui8i4u},
	{"i8ui8i8", __M2N_i8ui8i8},
	{"i8uu", __M2N_i8uu},
	{"i8uuu", __M2N_i8uuu},
	{"ii", __M2N_ii},
	{"ii4", __M2N_ii4},
	{"ii4ii", __M2N_ii4ii},
	{"ii8", __M2N_ii8},
	{"iii", __M2N_iii},
	{"iii4", __M2N_iii4},
	{"iiiu", __M2N_iiiu},
	{"iiiu1", __M2N_iiiu1},
	{"iiu", __M2N_iiu},
	{"iiuu", __M2N_iiuu},
	{"iiuu1", __M2N_iiuu1},
	{"iiuuu1", __M2N_iiuuu1},
	{"is123", __M2N_is123},
	{"iu", __M2N_iu},
	{"iu1u1uu", __M2N_iu1u1uu},
	{"iui", __M2N_iui},
	{"iui4i4i4i4u", __M2N_iui4i4i4i4u},
	{"iuii", __M2N_iuii},
	{"iuu", __M2N_iuu},
	{"r4", __M2N_r4},
	{"r4i", __M2N_r4i},
	{"r4i1", __M2N_r4i1},
	{"r4i2", __M2N_r4i2},
	{"r4i4", __M2N_r4i4},
	{"r4i8", __M2N_r4i8},
	{"r4ii", __M2N_r4ii},
	{"r4ii4", __M2N_r4ii4},
	{"r4iiu", __M2N_r4iiu},
	{"r4r4", __M2N_r4r4},
	{"r4r4r4", __M2N_r4r4r4},
	{"r4r4r4r4", __M2N_r4r4r4r4},
	{"r4r8", __M2N_r4r8},
	{"r4s16", __M2N_r4s16},
	{"r4u", __M2N_r4u},
	{"r4u1", __M2N_r4u1},
	{"r4u2", __M2N_r4u2},
	{"r4u4", __M2N_r4u4},
	{"r4u8", __M2N_r4u8},
	{"r4ui4u", __M2N_r4ui4u},
	{"r4ur4", __M2N_r4ur4},
	{"r4ur4r4", __M2N_r4ur4r4},
	{"r4uu", __M2N_r4uu},
	{"r4uuu", __M2N_r4uuu},
	{"r8", __M2N_r8},
	{"r8i", __M2N_r8i},
	{"r8i1", __M2N_r8i1},
	{"r8i2", __M2N_r8i2},
	{"r8i4", __M2N_r8i4},
	{"r8i8", __M2N_r8i8},
	{"r8ii", __M2N_r8ii},
	{"r8ii4", __M2N_r8ii4},
	{"r8iiu", __M2N_r8iiu},
	{"r8r4", __M2N_r8r4},
	{"r8r8", __M2N_r8r8},
	{"r8r8r8", __M2N_r8r8r8},
	{"r8s16", __M2N_r8s16},
	{"r8u", __M2N_r8u},
	{"r8u1", __M2N_r8u1},
	{"r8u2", __M2N_r8u2},
	{"r8u4", __M2N_r8u4},
	{"r8u8", __M2N_r8u8},
	{"r8ui4u", __M2N_r8ui4u},
	{"r8ur8", __M2N_r8ur8},
	{"r8ur8r8", __M2N_r8ur8r8},
	{"r8uu", __M2N_r8uu},
	{"r8uuu", __M2N_r8uuu},
	{"s0s0", __M2N_s0s0},
	{"s0u", __M2N_s0u},
	{"s0ui4", __M2N_s0ui4},
	{"s0ui4i4", __M2N_s0ui4i4},
	{"s0us0", __M2N_s0us0},
	{"s0uus117", __M2N_s0uus117},
	{"s105u", __M2N_s105u},
	{"s105ui4", __M2N_s105ui4},
	{"s10u", __M2N_s10u},
	{"s110u", __M2N_s110u},
	{"s111u", __M2N_s111u},
	{"s120u", __M2N_s120u},
	{"s123i", __M2N_s123i},
	{"s123u", __M2N_s123u},
	{"s123ui4", __M2N_s123ui4},
	{"s125", __M2N_s125},
	{"s125s125", __M2N_s125s125},
	{"s125s125s125", __M2N_s125s125s125},
	{"s125u", __M2N_s125u},
	{"s13ui4", __M2N_s13ui4},
	{"s15", __M2N_s15},
	{"s15u1", __M2N_s15u1},
	{"s15uu1", __M2N_s15uu1},
	{"s16i1", __M2N_s16i1},
	{"s16i2", __M2N_s16i2},
	{"s16i4", __M2N_s16i4},
	{"s16i8", __M2N_s16i8},
	{"s16r4", __M2N_s16r4},
	{"s16r8", __M2N_s16r8},
	{"s16s16i4", __M2N_s16s16i4},
	{"s16u", __M2N_s16u},
	{"s16u1", __M2N_s16u1},
	{"s16u2", __M2N_s16u2},
	{"s16u4", __M2N_s16u4},
	{"s16u8", __M2N_s16u8},
	{"s16ui4u", __M2N_s16ui4u},
	{"s16uu", __M2N_s16uu},
	{"s17", __M2N_s17},
	{"s17i8", __M2N_s17i8},
	{"s17s17i4", __M2N_s17s17i4},
	{"s17s17s22", __M2N_s17s17s22},
	{"s17u", __M2N_s17u},
	{"s17ui4", __M2N_s17ui4},
	{"s17ui4i4i4i4i4i4i4i4", __M2N_s17ui4i4i4i4i4i4i4i4},
	{"s17ui8", __M2N_s17ui8},
	{"s17ur8", __M2N_s17ur8},
	{"s17us22", __M2N_s17us22},
	{"s17uu", __M2N_s17uu},
	{"s18i8", __M2N_s18i8},
	{"s19", __M2N_s19},
	{"s19s0", __M2N_s19s0},
	{"s19u", __M2N_s19u},
	{"s22", __M2N_s22},
	{"s22i8", __M2N_s22i8},
	{"s22r8", __M2N_s22r8},
	{"s22s17s17", __M2N_s22s17s17},
	{"s22s22", __M2N_s22s22},
	{"s22s22s22", __M2N_s22s22s22},
	{"s22u", __M2N_s22u},
	{"s22us17", __M2N_s22us17},
	{"s22us22", __M2N_s22us22},
	{"s22uu", __M2N_s22uu},
	{"s23s17i4i4", __M2N_s23s17i4i4},
	{"s23s17i4i4i4", __M2N_s23s17i4i4i4},
	{"s23u", __M2N_s23u},
	{"s27ui4i4", __M2N_s27ui4i4},
	{"s30us46", __M2N_s30us46},
	{"s30us46i4", __M2N_s30us46i4},
	{"s31", __M2N_s31},
	{"s31r4", __M2N_s31r4},
	{"s31s31", __M2N_s31s31},
	{"s31s31r4", __M2N_s31s31r4},
	{"s31u", __M2N_s31u},
	{"s31ui4", __M2N_s31ui4},
	{"s35s46s31s46", __M2N_s35s46s31s46},
	{"s36", __M2N_s36},
	{"s36u", __M2N_s36u},
	{"s36us36u", __M2N_s36us36u},
	{"s3u", __M2N_s3u},
	{"s3ui4", __M2N_s3ui4},
	{"s45", __M2N_s45},
	{"s45s46", __M2N_s45s46},
	{"s45u", __M2N_s45u},
	{"s46", __M2N_s46},
	{"s46s45", __M2N_s46s45},
	{"s46s46", __M2N_s46s46},
	{"s46s46s46", __M2N_s46s46s46},
	{"s46u", __M2N_s46u},
	{"s4u", __M2N_s4u},
	{"s4uu", __M2N_s4uu},
	{"s55", __M2N_s55},
	{"s55u", __M2N_s55u},
	{"s56u", __M2N_s56u},
	{"s56uu", __M2N_s56uu},
	{"s63u", __M2N_s63u},
	{"s64u", __M2N_s64u},
	{"s66u", __M2N_s66u},
	{"s6i4", __M2N_s6i4},
	{"s6u", __M2N_s6u},
	{"s6ui4", __M2N_s6ui4},
	{"s71u", __M2N_s71u},
	{"s72u", __M2N_s72u},
	{"s74u", __M2N_s74u},
	{"s75u", __M2N_s75u},
	{"s76u", __M2N_s76u},
	{"s77u", __M2N_s77u},
	{"s78u", __M2N_s78u},
	{"s88u", __M2N_s88u},
	{"s88uu1", __M2N_s88uu1},
	{"s8u", __M2N_s8u},
	{"s8uuuu1u1", __M2N_s8uuuu1u1},
	{"s9", __M2N_s9},
	{"s9u", __M2N_s9u},
	{"s9ui4", __M2N_s9ui4},
	{"typedbyrefuu", __M2N_typedbyrefuu},
	{"u", __M2N_u},
	{"u1", __M2N_u1},
	{"u1i", __M2N_u1i},
	{"u1i1", __M2N_u1i1},
	{"u1i2", __M2N_u1i2},
	{"u1i4", __M2N_u1i4},
	{"u1i4i4", __M2N_u1i4i4},
	{"u1i4s0us0u", __M2N_u1i4s0us0u},
	{"u1i4u1", __M2N_u1i4u1},
	{"u1i4uuu", __M2N_u1i4uuu},
	{"u1i8", __M2N_u1i8},
	{"u1i8s0us0u", __M2N_u1i8s0us0u},
	{"u1ii", __M2N_u1ii},
	{"u1ii4", __M2N_u1ii4},
	{"u1iiu", __M2N_u1iiu},
	{"u1iu", __M2N_u1iu},
	{"u1r4", __M2N_u1r4},
	{"u1r4r4", __M2N_u1r4r4},
	{"u1r4s0us0u", __M2N_u1r4s0us0u},
	{"u1r8", __M2N_u1r8},
	{"u1r8s0us0u", __M2N_u1r8s0us0u},
	{"u1s0", __M2N_u1s0},
	{"u1s0s0", __M2N_u1s0s0},
	{"u1s0s0u", __M2N_u1s0s0u},
	{"u1s0u", __M2N_u1s0u},
	{"u1s0u4", __M2N_u1s0u4},
	{"u1s123s123", __M2N_u1s123s123},
	{"u1s125s125", __M2N_u1s125s125},
	{"u1s13s13", __M2N_u1s13s13},
	{"u1s16", __M2N_u1s16},
	{"u1s16s0us0u", __M2N_u1s16s0us0u},
	{"u1s16s16", __M2N_u1s16s16},
	{"u1s17s17", __M2N_u1s17s17},
	{"u1s19s19", __M2N_u1s19s19},
	{"u1s22s22", __M2N_u1s22s22},
	{"u1s23s23", __M2N_u1s23s23},
	{"u1s3", __M2N_u1s3},
	{"u1s36s36", __M2N_u1s36s36},
	{"u1s3s3", __M2N_u1s3s3},
	{"u1s45s45", __M2N_u1s45s45},
	{"u1s46s46", __M2N_u1s46s46},
	{"u1s55s55", __M2N_u1s55s55},
	{"u1u", __M2N_u1u},
	{"u1u1", __M2N_u1u1},
	{"u1u2", __M2N_u1u2},
	{"u1u2u2", __M2N_u1u2u2},
	{"u1u4", __M2N_u1u4},
	{"u1u4s0us0u", __M2N_u1u4s0us0u},
	{"u1u8", __M2N_u1u8},
	{"u1u8s0us0u", __M2N_u1u8s0us0u},
	{"u1u8u8", __M2N_u1u8u8},
	{"u1ui", __M2N_u1ui},
	{"u1ui1", __M2N_u1ui1},
	{"u1ui2", __M2N_u1ui2},
	{"u1ui4", __M2N_u1ui4},
	{"u1ui4i", __M2N_u1ui4i},
	{"u1ui4i4", __M2N_u1ui4i4},
	{"u1ui4s55", __M2N_u1ui4s55},
	{"u1ui4u", __M2N_u1ui4u},
	{"u1ui4u1", __M2N_u1ui4u1},
	{"u1ui4uu1", __M2N_u1ui4uu1},
	{"u1ui8", __M2N_u1ui8},
	{"u1uii", __M2N_u1uii},
	{"u1ur4", __M2N_u1ur4},
	{"u1ur8", __M2N_u1ur8},
	{"u1us0", __M2N_u1us0},
	{"u1us0i4u", __M2N_u1us0i4u},
	{"u1us0u", __M2N_u1us0u},
	{"u1us0us0", __M2N_u1us0us0},
	{"u1us0us0u", __M2N_u1us0us0u},
	{"u1us10s10", __M2N_u1us10s10},
	{"u1us123", __M2N_u1us123},
	{"u1us125", __M2N_u1us125},
	{"u1us13", __M2N_u1us13},
	{"u1us15", __M2N_u1us15},
	{"u1us16", __M2N_u1us16},
	{"u1us17", __M2N_u1us17},
	{"u1us18", __M2N_u1us18},
	{"u1us19", __M2N_u1us19},
	{"u1us19i4", __M2N_u1us19i4},
	{"u1us19u", __M2N_u1us19u},
	{"u1us19ui4", __M2N_u1us19ui4},
	{"u1us22", __M2N_u1us22},
	{"u1us22s22", __M2N_u1us22s22},
	{"u1us23", __M2N_u1us23},
	{"u1us25", __M2N_u1us25},
	{"u1us26", __M2N_u1us26},
	{"u1us27", __M2N_u1us27},
	{"u1us3", __M2N_u1us3},
	{"u1us30", __M2N_u1us30},
	{"u1us31", __M2N_u1us31},
	{"u1us35", __M2N_u1us35},
	{"u1us36", __M2N_u1us36},
	{"u1us3s3", __M2N_u1us3s3},
	{"u1us3u", __M2N_u1us3u},
	{"u1us3uu1", __M2N_u1us3uu1},
	{"u1us4", __M2N_u1us4},
	{"u1us42", __M2N_u1us42},
	{"u1us44", __M2N_u1us44},
	{"u1us45", __M2N_u1us45},
	{"u1us46", __M2N_u1us46},
	{"u1us4s4", __M2N_u1us4s4},
	{"u1us55", __M2N_u1us55},
	{"u1us56", __M2N_u1us56},
	{"u1us57", __M2N_u1us57},
	{"u1us59", __M2N_u1us59},
	{"u1us6", __M2N_u1us6},
	{"u1us6s6", __M2N_u1us6s6},
	{"u1us8", __M2N_u1us8},
	{"u1us9", __M2N_u1us9},
	{"u1us9s9", __M2N_u1us9s9},
	{"u1uu", __M2N_u1uu},
	{"u1uu1", __M2N_u1uu1},
	{"u1uu1u1", __M2N_u1uu1u1},
	{"u1uu2", __M2N_u1uu2},
	{"u1uu2i4", __M2N_u1uu2i4},
	{"u1uu2u2i4", __M2N_u1uu2u2i4},
	{"u1uu4", __M2N_u1uu4},
	{"u1uu4i4i4u", __M2N_u1uu4i4i4u},
	{"u1uu8", __M2N_u1uu8},
	{"u1uui4", __M2N_u1uui4},
	{"u1uui4u", __M2N_u1uui4u},
	{"u1uui4u1", __M2N_u1uui4u1},
	{"u1uui4uu1u1u", __M2N_u1uui4uu1u1u},
	{"u1uus4", __M2N_u1uus4},
	{"u1uus4u1", __M2N_u1uus4u1},
	{"u1uuu", __M2N_u1uuu},
	{"u1uuu1", __M2N_u1uuu1},
	{"u1uuu8", __M2N_u1uuu8},
	{"u1uuui4", __M2N_u1uuui4},
	{"u1uuui4i4i4", __M2N_u1uuui4i4i4},
	{"u1uuuu", __M2N_u1uuuu},
	{"u1uuuu1", __M2N_u1uuuu1},
	{"u1uuuu1u", __M2N_u1uuuu1u},
	{"u2", __M2N_u2},
	{"u2i", __M2N_u2i},
	{"u2i1", __M2N_u2i1},
	{"u2i2", __M2N_u2i2},
	{"u2i4", __M2N_u2i4},
	{"u2i8", __M2N_u2i8},
	{"u2ii", __M2N_u2ii},
	{"u2ii4", __M2N_u2ii4},
	{"u2iiu", __M2N_u2iiu},
	{"u2r4", __M2N_u2r4},
	{"u2r8", __M2N_u2r8},
	{"u2s16", __M2N_u2s16},
	{"u2u", __M2N_u2u},
	{"u2u1", __M2N_u2u1},
	{"u2u2", __M2N_u2u2},
	{"u2u4", __M2N_u2u4},
	{"u2u8", __M2N_u2u8},
	{"u2ui4", __M2N_u2ui4},
	{"u2ui4u", __M2N_u2ui4u},
	{"u2uu", __M2N_u2uu},
	{"u2uu2", __M2N_u2uu2},
	{"u2uuu", __M2N_u2uuu},
	{"u4", __M2N_u4},
	{"u4i1", __M2N_u4i1},
	{"u4i2", __M2N_u4i2},
	{"u4i4", __M2N_u4i4},
	{"u4i8", __M2N_u4i8},
	{"u4r4", __M2N_u4r4},
	{"u4r8", __M2N_u4r8},
	{"u4s16", __M2N_u4s16},
	{"u4u", __M2N_u4u},
	{"u4u1", __M2N_u4u1},
	{"u4u2", __M2N_u4u2},
	{"u4u4", __M2N_u4u4},
	{"u4u8", __M2N_u4u8},
	{"u4ui4u", __M2N_u4ui4u},
	{"u4uu", __M2N_u4uu},
	{"u8", __M2N_u8},
	{"u8i1", __M2N_u8i1},
	{"u8i2", __M2N_u8i2},
	{"u8i4", __M2N_u8i4},
	{"u8i8", __M2N_u8i8},
	{"u8r4", __M2N_u8r4},
	{"u8r8", __M2N_u8r8},
	{"u8s16", __M2N_u8s16},
	{"u8u", __M2N_u8u},
	{"u8u1", __M2N_u8u1},
	{"u8u2", __M2N_u8u2},
	{"u8u4", __M2N_u8u4},
	{"u8ui4", __M2N_u8ui4},
	{"u8ui4u", __M2N_u8ui4u},
	{"u8uu", __M2N_u8uu},
	{"ui", __M2N_ui},
	{"ui4", __M2N_ui4},
	{"ui4s0u", __M2N_ui4s0u},
	{"ui4s27u", __M2N_ui4s27u},
	{"ui4s55", __M2N_ui4s55},
	{"ui4s59u", __M2N_ui4s59u},
	{"ui4uu", __M2N_ui4uu},
	{"ui8s0u", __M2N_ui8s0u},
	{"uii", __M2N_uii},
	{"uiiu", __M2N_uiiu},
	{"ur4", __M2N_ur4},
	{"ur4uu", __M2N_ur4uu},
	{"ur8uu", __M2N_ur8uu},
	{"us0", __M2N_us0},
	{"us0s0", __M2N_us0s0},
	{"us10", __M2N_us10},
	{"us123", __M2N_us123},
	{"us123s123", __M2N_us123s123},
	{"us16s0u", __M2N_us16s0u},
	{"us17s17s22s23s23", __M2N_us17s17s22s23s23},
	{"us55", __M2N_us55},
	{"us55s55", __M2N_us55s55},
	{"us74", __M2N_us74},
	{"us75", __M2N_us75},
	{"us76", __M2N_us76},
	{"us77", __M2N_us77},
	{"us78", __M2N_us78},
	{"uu", __M2N_uu},
	{"uu1", __M2N_uu1},
	{"uu2", __M2N_uu2},
	{"uu4", __M2N_uu4},
	{"uu4s0u", __M2N_uu4s0u},
	{"uu8", __M2N_uu8},
	{"uu8s0u", __M2N_uu8s0u},
	{"uui", __M2N_uui},
	{"uui1", __M2N_uui1},
	{"uui2", __M2N_uui2},
	{"uui4", __M2N_uui4},
	{"uui4i4", __M2N_uui4i4},
	{"uui4i4i4", __M2N_uui4i4i4},
	{"uui4s55", __M2N_uui4s55},
	{"uui4u", __M2N_uui4u},
	{"uui4u1", __M2N_uui4u1},
	{"uui4u2", __M2N_uui4u2},
	{"uui4ui4uu", __M2N_uui4ui4uu},
	{"uui4uu", __M2N_uui4uu},
	{"uui4uuu", __M2N_uui4uuu},
	{"uui4uuuu", __M2N_uui4uuuu},
	{"uui4uuuuuu", __M2N_uui4uuuuuu},
	{"uui8", __M2N_uui8},
	{"uui8i8", __M2N_uui8i8},
	{"uui8i8i8", __M2N_uui8i8i8},
	{"uus0", __M2N_uus0},
	{"uus19", __M2N_uus19},
	{"uus22uu", __M2N_uus22uu},
	{"uus22uuuuu1", __M2N_uus22uuuuu1},
	{"uus3", __M2N_uus3},
	{"uus4", __M2N_uus4},
	{"uuu", __M2N_uuu},
	{"uuu1", __M2N_uuu1},
	{"uuu1i4i4i4ui4i4i4i4u1u", __M2N_uuu1i4i4i4ui4i4i4i4u1u},
	{"uuu1u1", __M2N_uuu1u1},
	{"uuu2", __M2N_uuu2},
	{"uuu2i4", __M2N_uuu2i4},
	{"uuu2u2", __M2N_uuu2u2},
	{"uuu4", __M2N_uuu4},
	{"uuu8", __M2N_uuu8},
	{"uuui4", __M2N_uuui4},
	{"uuui4i4", __M2N_uuui4i4},
	{"uuui4i4i4", __M2N_uuui4i4i4},
	{"uuui4i4uu", __M2N_uuui4i4uu},
	{"uuui4ui4uu", __M2N_uuui4ui4uu},
	{"uuui4uuu", __M2N_uuui4uuu},
	{"uuui4uuuu", __M2N_uuui4uuuu},
	{"uuui4uuuuuu", __M2N_uuui4uuuuuu},
	{"uuus4u", __M2N_uuus4u},
	{"uuuu", __M2N_uuuu},
	{"uuuu1", __M2N_uuuu1},
	{"uuuu1u1", __M2N_uuuu1u1},
	{"uuuui4", __M2N_uuuui4},
	{"uuuus22u1", __M2N_uuuus22u1},
	{"uuuus4u", __M2N_uuuus4u},
	{"uuuus55i4u", __M2N_uuuus55i4u},
	{"uuuuu", __M2N_uuuuu},
	{"uuuuu1", __M2N_uuuuu1},
	{"uuuuu1u1", __M2N_uuuuu1u1},
	{"uuuuuu", __M2N_uuuuuu},
	{"v", __M2N_v},
	{"vi", __M2N_vi},
	{"vi4", __M2N_vi4},
	{"vi4i4", __M2N_vi4i4},
	{"vi4i4i4", __M2N_vi4i4i4},
	{"vi4i4i4i4i4", __M2N_vi4i4i4i4i4},
	{"vi4i4uuu", __M2N_vi4i4uuu},
	{"vi4ui4", __M2N_vi4ui4},
	{"vi4uui4i4", __M2N_vi4uui4i4},
	{"vi4uuu8", __M2N_vi4uuu8},
	{"vii4i", __M2N_vii4i},
	{"vii4i1", __M2N_vii4i1},
	{"vii4i2", __M2N_vii4i2},
	{"vii4i4", __M2N_vii4i4},
	{"vii4i8", __M2N_vii4i8},
	{"vii4r4", __M2N_vii4r4},
	{"vii4r8", __M2N_vii4r8},
	{"vii4u1", __M2N_vii4u1},
	{"vii4u2", __M2N_vii4u2},
	{"viii", __M2N_viii},
	{"viii1", __M2N_viii1},
	{"viii2", __M2N_viii2},
	{"viii4", __M2N_viii4},
	{"viii8", __M2N_viii8},
	{"viir4", __M2N_viir4},
	{"viir8", __M2N_viir8},
	{"viiu", __M2N_viiu},
	{"viiu1", __M2N_viiu1},
	{"viiu2", __M2N_viiu2},
	{"viui4i4", __M2N_viui4i4},
	{"viuu1", __M2N_viuu1},
	{"vs22", __M2N_vs22},
	{"vs31uu", __M2N_vs31uu},
	{"vs35", __M2N_vs35},
	{"vs46s46", __M2N_vs46s46},
	{"vs46s46s31", __M2N_vs46s46s31},
	{"vs46s46s31r4", __M2N_vs46s46s31r4},
	{"vs46s46s31r4u1", __M2N_vs46s46s31r4u1},
	{"vtypedbyrefu", __M2N_vtypedbyrefu},
	{"vu", __M2N_vu},
	{"vu1", __M2N_vu1},
	{"vu1u", __M2N_vu1u},
	{"vu1uu", __M2N_vu1uu},
	{"vu1uuu", __M2N_vu1uuu},
	{"vui", __M2N_vui},
	{"vui2", __M2N_vui2},
	{"vui4", __M2N_vui4},
	{"vui4i4", __M2N_vui4i4},
	{"vui4i4i4", __M2N_vui4i4i4},
	{"vui4i4i4i4", __M2N_vui4i4i4i4},
	{"vui4i4i4i4i4", __M2N_vui4i4i4i4i4},
	{"vui4i4i4i4i4i", __M2N_vui4i4i4i4i4i},
	{"vui4i4i4i4i4i4", __M2N_vui4i4i4i4i4i4},
	{"vui4i4i4i4i4i4i4", __M2N_vui4i4i4i4i4i4i4},
	{"vui4i4i4i4i4u1", __M2N_vui4i4i4i4i4u1},
	{"vui4i4i4i4u1", __M2N_vui4i4i4i4u1},
	{"vui4i4i4i4u1i", __M2N_vui4i4i4i4u1i},
	{"vui4i4i4i4u1u1", __M2N_vui4i4i4i4u1u1},
	{"vui4i4i4u", __M2N_vui4i4i4u},
	{"vui4i4i4u1", __M2N_vui4i4i4u1},
	{"vui4i4i4u1u1", __M2N_vui4i4i4u1u1},
	{"vui4i4u", __M2N_vui4i4u},
	{"vui4i4u1", __M2N_vui4i4u1},
	{"vui4i4u1u", __M2N_vui4i4u1u},
	{"vui4i4uuu", __M2N_vui4i4uuu},
	{"vui4r4", __M2N_vui4r4},
	{"vui4r4u", __M2N_vui4r4u},
	{"vui4s0i4", __M2N_vui4s0i4},
	{"vui4s105", __M2N_vui4s105},
	{"vui4s13", __M2N_vui4s13},
	{"vui4s3", __M2N_vui4s3},
	{"vui4s45", __M2N_vui4s45},
	{"vui4s6", __M2N_vui4s6},
	{"vui4s9", __M2N_vui4s9},
	{"vui4u", __M2N_vui4u},
	{"vui4u1", __M2N_vui4u1},
	{"vui4u2", __M2N_vui4u2},
	{"vui4ui4i4", __M2N_vui4ui4i4},
	{"vui4uu", __M2N_vui4uu},
	{"vui4uuu", __M2N_vui4uuu},
	{"vui4uuuuu", __M2N_vui4uuuuu},
	{"vui8", __M2N_vui8},
	{"vui8i4", __M2N_vui8i4},
	{"vui8s22", __M2N_vui8s22},
	{"vui8ui8", __M2N_vui8ui8},
	{"vui8ui8i8", __M2N_vui8ui8i8},
	{"vui8uu", __M2N_vui8uu},
	{"vuii4ii4u1", __M2N_vuii4ii4u1},
	{"vuiu1", __M2N_vuiu1},
	{"vur4", __M2N_vur4},
	{"vur4r4", __M2N_vur4r4},
	{"vur4r4r4", __M2N_vur4r4r4},
	{"vur4r4r4r4", __M2N_vur4r4r4r4},
	{"vur4r4r4r4u", __M2N_vur4r4r4r4u},
	{"vur8", __M2N_vur8},
	{"vus0", __M2N_vus0},
	{"vus0i8", __M2N_vus0i8},
	{"vus0s27", __M2N_vus0s27},
	{"vus0s59", __M2N_vus0s59},
	{"vus0u", __M2N_vus0u},
	{"vus106", __M2N_vus106},
	{"vus110", __M2N_vus110},
	{"vus123", __M2N_vus123},
	{"vus123u", __M2N_vus123u},
	{"vus123uu", __M2N_vus123uu},
	{"vus13", __M2N_vus13},
	{"vus15", __M2N_vus15},
	{"vus16", __M2N_vus16},
	{"vus17", __M2N_vus17},
	{"vus17s17s22", __M2N_vus17s17s22},
	{"vus19", __M2N_vus19},
	{"vus19u", __M2N_vus19u},
	{"vus19ui4", __M2N_vus19ui4},
	{"vus22", __M2N_vus22},
	{"vus27", __M2N_vus27},
	{"vus3", __M2N_vus3},
	{"vus31", __M2N_vus31},
	{"vus31s31s31s31", __M2N_vus31s31s31s31},
	{"vus31uu1u1u1u1", __M2N_vus31uu1u1u1u1},
	{"vus36", __M2N_vus36},
	{"vus36s108", __M2N_vus36s108},
	{"vus36s108u", __M2N_vus36s108u},
	{"vus36uu", __M2N_vus36uu},
	{"vus3u", __M2N_vus3u},
	{"vus4", __M2N_vus4},
	{"vus44", __M2N_vus44},
	{"vus44i4", __M2N_vus44i4},
	{"vus44s44", __M2N_vus44s44},
	{"vus45s45", __M2N_vus45s45},
	{"vus55i4i4u", __M2N_vus55i4i4u},
	{"vus56", __M2N_vus56},
	{"vus6", __M2N_vus6},
	{"vus8", __M2N_vus8},
	{"vus9", __M2N_vus9},
	{"vutypedbyrefu", __M2N_vutypedbyrefu},
	{"vuu", __M2N_vuu},
	{"vuu1", __M2N_vuu1},
	{"vuu1i4", __M2N_vuu1i4},
	{"vuu1i4i4s0", __M2N_vuu1i4i4s0},
	{"vuu1i4i4s55", __M2N_vuu1i4i4s55},
	{"vuu1i4u", __M2N_vuu1i4u},
	{"vuu1s56i4s55", __M2N_vuu1s56i4s55},
	{"vuu1u", __M2N_vuu1u},
	{"vuu1u1", __M2N_vuu1u1},
	{"vuu1u1i4s55", __M2N_vuu1u1i4s55},
	{"vuu1u1i4u1", __M2N_vuu1u1i4u1},
	{"vuu1u1u1", __M2N_vuu1u1u1},
	{"vuu1u1u4u4u4", __M2N_vuu1u1u4u4u4},
	{"vuu1u1uuuui4i4u", __M2N_vuu1u1uuuui4i4u},
	{"vuu1ui4s55", __M2N_vuu1ui4s55},
	{"vuu1uu1u1", __M2N_vuu1uu1u1},
	{"vuu1uuu", __M2N_vuu1uuu},
	{"vuu2", __M2N_vuu2},
	{"vuu2i4", __M2N_vuu2i4},
	{"vuu2i4i4i4i4i4i4i4", __M2N_vuu2i4i4i4i4i4i4i4},
	{"vuu2i4u1u1u1", __M2N_vuu2i4u1u1u1},
	{"vuu4", __M2N_vuu4},
	{"vuu4i4i4i4", __M2N_vuu4i4i4i4},
	{"vuu4u4u4", __M2N_vuu4u4u4},
	{"vuu8", __M2N_vuu8},
	{"vuu8u", __M2N_vuu8u},
	{"vuui", __M2N_vuui},
	{"vuui2", __M2N_vuui2},
	{"vuui4", __M2N_vuui4},
	{"vuui4i4", __M2N_vuui4i4},
	{"vuui4i4i4", __M2N_vuui4i4i4},
	{"vuui4i4i4i4", __M2N_vuui4i4i4i4},
	{"vuui4i4i4i4i4", __M2N_vuui4i4i4i4i4},
	{"vuui4i4i4u", __M2N_vuui4i4i4u},
	{"vuui4i4u", __M2N_vuui4i4u},
	{"vuui4u1", __M2N_vuui4u1},
	{"vuui8", __M2N_vuui8},
	{"vuui8i8", __M2N_vuui8i8},
	{"vuui8i8i4", __M2N_vuui8i8i4},
	{"vuui8i8i8", __M2N_vuui8i8i8},
	{"vuui8ui8uu", __M2N_vuui8ui8uu},
	{"vuuiu", __M2N_vuuiu},
	{"vuur4", __M2N_vuur4},
	{"vuur4r4", __M2N_vuur4r4},
	{"vuus17", __M2N_vuus17},
	{"vuus27", __M2N_vuus27},
	{"vuus3", __M2N_vuus3},
	{"vuus4", __M2N_vuus4},
	{"vuus55", __M2N_vuus55},
	{"vuuu", __M2N_vuuu},
	{"vuuu1", __M2N_vuuu1},
	{"vuuu1i4", __M2N_vuuu1i4},
	{"vuuu1s104", __M2N_vuuu1s104},
	{"vuuu1u1", __M2N_vuuu1u1},
	{"vuuu4", __M2N_vuuu4},
	{"vuuu8", __M2N_vuuu8},
	{"vuuui4", __M2N_vuuui4},
	{"vuuui4i4", __M2N_vuuui4i4},
	{"vuuui4i4u", __M2N_vuuui4i4u},
	{"vuuui4u", __M2N_vuuui4u},
	{"vuuui4u1", __M2N_vuuui4u1},
	{"vuuui4uu", __M2N_vuuui4uu},
	{"vuuur4", __M2N_vuuur4},
	{"vuuus22s22", __M2N_vuuus22s22},
	{"vuuus4", __M2N_vuuus4},
	{"vuuus55i4", __M2N_vuuus55i4},
	{"vuuuu", __M2N_vuuuu},
	{"vuuuu1", __M2N_vuuuu1},
	{"vuuuu1i4", __M2N_vuuuu1i4},
	{"vuuuu1i4u1", __M2N_vuuuu1i4u1},
	{"vuuuui4", __M2N_vuuuui4},
	{"vuuuui4i4", __M2N_vuuuui4i4},
	{"vuuuus55i4i4u", __M2N_vuuuus55i4i4u},
	{"vuuuuu", __M2N_vuuuuu},
	{"vuuuuui4u", __M2N_vuuuuui4u},
	{"vuuuuuu", __M2N_vuuuuuu},
	{nullptr, nullptr},
};

static int8_t __N2M_i1u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    int8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int8_t __N2M_i1uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int16_t __N2M_i2u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    int16_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int16_t __N2M_i2uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int16_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4(const MethodInfo* method)
{
    
	constexpr int __TOTAL_ARG_SIZE__ = 1;

    StackObject args[__TOTAL_ARG_SIZE__];

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4i4i4(int32_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(int32_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4s6s6(__struct_6__ __arg0, __struct_6__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_6__*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_6__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4s9s9(__struct_9__ __arg0, __struct_9__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_9__*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_9__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4u8u8(uint64_t __arg0, uint64_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uint64_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint64_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4ui(uintptr_t __arg0, intptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(intptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4ui1(uintptr_t __arg0, int8_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4ui2(uintptr_t __arg0, int16_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4ui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4ui4i4(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4ui4i4i4(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4ui8(uintptr_t __arg0, int64_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int64_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uii(uintptr_t __arg0, intptr_t __arg1, intptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(intptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(intptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4ur4(uintptr_t __arg0, float __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(float*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4ur8(uintptr_t __arg0, double __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(double*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us0(uintptr_t __arg0, __struct_0__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_0__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us0s0(uintptr_t __arg0, __struct_0__ __arg1, __struct_0__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_0__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_0__*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us10(uintptr_t __arg0, __struct_10__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_10__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us10s10(uintptr_t __arg0, __struct_10__ __arg1, __struct_10__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_10__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_10__*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us13(uintptr_t __arg0, __struct_13__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_13__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us16(uintptr_t __arg0, __struct_16__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_16__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us17(uintptr_t __arg0, __struct_17__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_17__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us18(uintptr_t __arg0, __struct_18__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_18__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us19(uintptr_t __arg0, __struct_19__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_19__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us22(uintptr_t __arg0, __struct_22__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_22__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us25(uintptr_t __arg0, __struct_25__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_25__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us26(uintptr_t __arg0, __struct_26__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_26__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us27(uintptr_t __arg0, __struct_27__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_27__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us3(uintptr_t __arg0, __struct_3__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_3__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us4(uintptr_t __arg0, __struct_4__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_4__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us57(uintptr_t __arg0, __struct_57__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_57__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us59(uintptr_t __arg0, __struct_59__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_59__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us6(uintptr_t __arg0, __struct_6__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_6__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us6s6(uintptr_t __arg0, __struct_6__ __arg1, __struct_6__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_6__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_6__*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us9(uintptr_t __arg0, __struct_9__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_9__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4us9s9(uintptr_t __arg0, __struct_9__ __arg1, __struct_9__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_9__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_9__*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uu1(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uu1u1(uintptr_t __arg0, uint8_t __arg1, uint8_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uu2(uintptr_t __arg0, uint16_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uu4(uintptr_t __arg0, uint32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uu8(uintptr_t __arg0, uint64_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint64_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uu8u8(uintptr_t __arg0, uint64_t __arg1, uint64_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint64_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uint64_t*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uui4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uui4i4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uui4i4i4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uui4i4u1(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uint8_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	args[__ARG_OFFSET_4__].u64 = __arg4;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uui4i4ui4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, int32_t __arg5, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(int32_t*)(args + __ARG_OFFSET_5__) = __arg5;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uui4i4ui4i4i4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, int32_t __arg5, int32_t __arg6, int32_t __arg7, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __ARG_OFFSET_6__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;
	constexpr int __ARG_SIZE_6__ = (sizeof(__arg6) + 7)/8;
	constexpr int __ARG_OFFSET_7__ = __ARG_OFFSET_6__ + __ARG_SIZE_6__;
	constexpr int __ARG_SIZE_7__ = (sizeof(__arg7) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_7__ + __ARG_SIZE_7__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(int32_t*)(args + __ARG_OFFSET_5__) = __arg5;
	*(int32_t*)(args + __ARG_OFFSET_6__) = __arg6;
	*(int32_t*)(args + __ARG_OFFSET_7__) = __arg7;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uui4i4ui4u1(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, int32_t __arg5, uint8_t __arg6, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __ARG_OFFSET_6__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;
	constexpr int __ARG_SIZE_6__ = (sizeof(__arg6) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_6__ + __ARG_SIZE_6__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(int32_t*)(args + __ARG_OFFSET_5__) = __arg5;
	args[__ARG_OFFSET_6__].u64 = __arg6;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uui4u1(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uint8_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	args[__ARG_OFFSET_3__].u64 = __arg3;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uui4ui4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, int32_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uui4ui4u1(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, int32_t __arg4, uint8_t __arg5, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;
	args[__ARG_OFFSET_5__].u64 = __arg5;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uuii4i4(uintptr_t __arg0, uintptr_t __arg1, intptr_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(intptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uus10i4i4(uintptr_t __arg0, uintptr_t __arg1, __struct_10__ __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_10__*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uus3i4i4(uintptr_t __arg0, uintptr_t __arg1, __struct_3__ __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_3__*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uus4i4i4(uintptr_t __arg0, uintptr_t __arg1, __struct_4__ __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_4__*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uus6i4i4(uintptr_t __arg0, uintptr_t __arg1, __struct_6__ __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_6__*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uus9i4i4(uintptr_t __arg0, uintptr_t __arg1, __struct_9__ __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_9__*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uuu1i4(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uuu1i4i4(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uuui4(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uuui4i4(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, int32_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_i4uuui4i4i4(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(int32_t*)(args + __ARG_OFFSET_5__) = __arg5;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int64_t __N2M_i8u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    int64_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int64_t __N2M_i8ui8i4(uintptr_t __arg0, int64_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int64_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    int64_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int64_t __N2M_i8uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int64_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int64_t __N2M_i8uuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    int64_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static intptr_t __N2M_iuu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    intptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static float __N2M_r4u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    float ret; Interpreter::Execute(method, args, &ret); return ret;
}


static float __N2M_r4uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    float ret; Interpreter::Execute(method, args, &ret); return ret;
}


static double __N2M_r8u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    double ret; Interpreter::Execute(method, args, &ret); return ret;
}


static double __N2M_r8uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    double ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_0__ __N2M_s0u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_0__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_0__ __N2M_s0us117(uintptr_t __arg0, __struct_117__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_117__*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_0__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_0__ __N2M_s0uus117(uintptr_t __arg0, uintptr_t __arg1, __struct_117__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_117__*)(args + __ARG_OFFSET_2__) = __arg2;

    __struct_0__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_105__ __N2M_s105u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_105__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_111__ __N2M_s111(const MethodInfo* method)
{
    
	constexpr int __TOTAL_ARG_SIZE__ = 1;

    StackObject args[__TOTAL_ARG_SIZE__];

    __struct_111__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_111__ __N2M_s111u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_111__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_123__ __N2M_s123u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_123__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_13__ __N2M_s13ui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_13__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_15__ __N2M_s15uu1(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    __struct_15__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_16__ __N2M_s16u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_16__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_16__ __N2M_s16uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_16__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_17__ __N2M_s17u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_17__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_17__ __N2M_s17ui4i4i4i4i4i4i4i4(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, int32_t __arg6, int32_t __arg7, int32_t __arg8, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __ARG_OFFSET_6__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;
	constexpr int __ARG_SIZE_6__ = (sizeof(__arg6) + 7)/8;
	constexpr int __ARG_OFFSET_7__ = __ARG_OFFSET_6__ + __ARG_SIZE_6__;
	constexpr int __ARG_SIZE_7__ = (sizeof(__arg7) + 7)/8;
	constexpr int __ARG_OFFSET_8__ = __ARG_OFFSET_7__ + __ARG_SIZE_7__;
	constexpr int __ARG_SIZE_8__ = (sizeof(__arg8) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_8__ + __ARG_SIZE_8__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(int32_t*)(args + __ARG_OFFSET_5__) = __arg5;
	*(int32_t*)(args + __ARG_OFFSET_6__) = __arg6;
	*(int32_t*)(args + __ARG_OFFSET_7__) = __arg7;
	*(int32_t*)(args + __ARG_OFFSET_8__) = __arg8;

    __struct_17__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_17__ __N2M_s17uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_17__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_19__ __N2M_s19u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_19__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_22__ __N2M_s22u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_22__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_22__ __N2M_s22us22(uintptr_t __arg0, __struct_22__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_22__*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_22__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_22__ __N2M_s22uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_22__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_36__ __N2M_s36s36u(__struct_36__ __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_36__*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_36__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_36__ __N2M_s36u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_36__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_36__ __N2M_s36us36u(uintptr_t __arg0, __struct_36__ __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_36__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    __struct_36__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_3__ __N2M_s3u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_3__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_3__ __N2M_s3ui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_3__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_4__ __N2M_s4u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_4__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_56__ __N2M_s56(const MethodInfo* method)
{
    
	constexpr int __TOTAL_ARG_SIZE__ = 1;

    StackObject args[__TOTAL_ARG_SIZE__];

    __struct_56__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_56__ __N2M_s56u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_56__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_56__ __N2M_s56uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_56__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_6__ __N2M_s6u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_6__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_6__ __N2M_s6ui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_6__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_74__ __N2M_s74u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_74__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_8__ __N2M_s8u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_8__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_9__ __N2M_s9u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_9__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_9__ __N2M_s9ui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_9__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_u(const MethodInfo* method)
{
    
	constexpr int __TOTAL_ARG_SIZE__ = 1;

    StackObject args[__TOTAL_ARG_SIZE__];

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1(const MethodInfo* method)
{
    
	constexpr int __TOTAL_ARG_SIZE__ = 1;

    StackObject args[__TOTAL_ARG_SIZE__];

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1i4(int32_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(int32_t*)(args + __ARG_OFFSET_0__) = __arg0;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1i4i(int32_t __arg0, intptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(int32_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(intptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1s6(__struct_6__ __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_6__*)(args + __ARG_OFFSET_0__) = __arg0;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1s9(__struct_9__ __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_9__*)(args + __ARG_OFFSET_0__) = __arg0;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1u2(uint16_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	args[__ARG_OFFSET_0__].u64 = __arg0;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1ui(uintptr_t __arg0, intptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(intptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1ui1(uintptr_t __arg0, int8_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1ui2(uintptr_t __arg0, int16_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1ui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1ui4i(uintptr_t __arg0, int32_t __arg1, intptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(intptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1ui4i4(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1ui4u(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1ui4u1(uintptr_t __arg0, int32_t __arg1, uint8_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1ui8(uintptr_t __arg0, int64_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int64_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uii(uintptr_t __arg0, intptr_t __arg1, intptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(intptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(intptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1ur4(uintptr_t __arg0, float __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(float*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1ur8(uintptr_t __arg0, double __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(double*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us0us0u(uintptr_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, __struct_0__ __arg3, uintptr_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_0__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(__struct_0__*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us10s10(uintptr_t __arg0, __struct_10__ __arg1, __struct_10__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_10__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_10__*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us123(uintptr_t __arg0, __struct_123__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_123__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us125(uintptr_t __arg0, __struct_125__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_125__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us13(uintptr_t __arg0, __struct_13__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_13__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us16(uintptr_t __arg0, __struct_16__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_16__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us17(uintptr_t __arg0, __struct_17__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_17__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us18(uintptr_t __arg0, __struct_18__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_18__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us19(uintptr_t __arg0, __struct_19__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_19__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us19u(uintptr_t __arg0, __struct_19__ __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_19__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us19ui4(uintptr_t __arg0, __struct_19__ __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_19__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us22(uintptr_t __arg0, __struct_22__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_22__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us23(uintptr_t __arg0, __struct_23__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_23__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us25(uintptr_t __arg0, __struct_25__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_25__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us26(uintptr_t __arg0, __struct_26__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_26__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us27(uintptr_t __arg0, __struct_27__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_27__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us3(uintptr_t __arg0, __struct_3__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_3__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us30(uintptr_t __arg0, __struct_30__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_30__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us31(uintptr_t __arg0, __struct_31__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_31__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us35(uintptr_t __arg0, __struct_35__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_35__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us36(uintptr_t __arg0, __struct_36__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_36__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us3s3(uintptr_t __arg0, __struct_3__ __arg1, __struct_3__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_3__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_3__*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us3u(uintptr_t __arg0, __struct_3__ __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_3__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us4(uintptr_t __arg0, __struct_4__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_4__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us42(uintptr_t __arg0, __struct_42__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_42__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us44(uintptr_t __arg0, __struct_44__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_44__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us45(uintptr_t __arg0, __struct_45__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_45__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us46(uintptr_t __arg0, __struct_46__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_46__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us4s4(uintptr_t __arg0, __struct_4__ __arg1, __struct_4__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_4__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_4__*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us57(uintptr_t __arg0, __struct_57__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_57__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us59(uintptr_t __arg0, __struct_59__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_59__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us6(uintptr_t __arg0, __struct_6__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_6__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us6s6(uintptr_t __arg0, __struct_6__ __arg1, __struct_6__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_6__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_6__*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us8(uintptr_t __arg0, __struct_8__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_8__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us9(uintptr_t __arg0, __struct_9__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_9__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1us9s9(uintptr_t __arg0, __struct_9__ __arg1, __struct_9__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_9__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_9__*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uu1(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uu1u1(uintptr_t __arg0, uint8_t __arg1, uint8_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uu2(uintptr_t __arg0, uint16_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uu2i4(uintptr_t __arg0, uint16_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uu2u2i4(uintptr_t __arg0, uint16_t __arg1, uint16_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uu4(uintptr_t __arg0, uint32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uu8(uintptr_t __arg0, uint64_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint64_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uui4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uuu1(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_u1uuui4(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint16_t __N2M_u2u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    uint16_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint16_t __N2M_u2uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint16_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint16_t __N2M_u2uu2(uintptr_t __arg0, uint16_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    uint16_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint32_t __N2M_u4u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    uint32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint32_t __N2M_u4uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint64_t __N2M_u8u(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    uint64_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint64_t __N2M_u8uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint64_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_us3(__struct_3__ __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_3__*)(args + __ARG_OFFSET_0__) = __arg0;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uu(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uui(uintptr_t __arg0, intptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(intptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uui4ui4uu(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, int32_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(uintptr_t*)(args + __ARG_OFFSET_5__) = __arg5;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uui4uu(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uui4uuu(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uui4uuuu(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(uintptr_t*)(args + __ARG_OFFSET_5__) = __arg5;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uui4uuuuuu(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, uintptr_t __arg6, uintptr_t __arg7, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __ARG_OFFSET_6__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;
	constexpr int __ARG_SIZE_6__ = (sizeof(__arg6) + 7)/8;
	constexpr int __ARG_OFFSET_7__ = __ARG_OFFSET_6__ + __ARG_SIZE_6__;
	constexpr int __ARG_SIZE_7__ = (sizeof(__arg7) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_7__ + __ARG_SIZE_7__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(uintptr_t*)(args + __ARG_OFFSET_5__) = __arg5;
	*(uintptr_t*)(args + __ARG_OFFSET_6__) = __arg6;
	*(uintptr_t*)(args + __ARG_OFFSET_7__) = __arg7;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uui8(uintptr_t __arg0, int64_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int64_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uus3(uintptr_t __arg0, __struct_3__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_3__*)(args + __ARG_OFFSET_1__) = __arg1;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uus4(uintptr_t __arg0, __struct_4__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_4__*)(args + __ARG_OFFSET_1__) = __arg1;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuu1(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuui4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuui4i4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuui4i4uu(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(uintptr_t*)(args + __ARG_OFFSET_5__) = __arg5;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuui4ui4uu(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, int32_t __arg4, uintptr_t __arg5, uintptr_t __arg6, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __ARG_OFFSET_6__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;
	constexpr int __ARG_SIZE_6__ = (sizeof(__arg6) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_6__ + __ARG_SIZE_6__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(uintptr_t*)(args + __ARG_OFFSET_5__) = __arg5;
	*(uintptr_t*)(args + __ARG_OFFSET_6__) = __arg6;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuui4uuu(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(uintptr_t*)(args + __ARG_OFFSET_5__) = __arg5;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuui4uuuu(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, uintptr_t __arg6, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __ARG_OFFSET_6__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;
	constexpr int __ARG_SIZE_6__ = (sizeof(__arg6) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_6__ + __ARG_SIZE_6__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(uintptr_t*)(args + __ARG_OFFSET_5__) = __arg5;
	*(uintptr_t*)(args + __ARG_OFFSET_6__) = __arg6;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuui4uuuuuu(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, uintptr_t __arg3, uintptr_t __arg4, uintptr_t __arg5, uintptr_t __arg6, uintptr_t __arg7, uintptr_t __arg8, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __ARG_OFFSET_6__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;
	constexpr int __ARG_SIZE_6__ = (sizeof(__arg6) + 7)/8;
	constexpr int __ARG_OFFSET_7__ = __ARG_OFFSET_6__ + __ARG_SIZE_6__;
	constexpr int __ARG_SIZE_7__ = (sizeof(__arg7) + 7)/8;
	constexpr int __ARG_OFFSET_8__ = __ARG_OFFSET_7__ + __ARG_SIZE_7__;
	constexpr int __ARG_SIZE_8__ = (sizeof(__arg8) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_8__ + __ARG_SIZE_8__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(uintptr_t*)(args + __ARG_OFFSET_5__) = __arg5;
	*(uintptr_t*)(args + __ARG_OFFSET_6__) = __arg6;
	*(uintptr_t*)(args + __ARG_OFFSET_7__) = __arg7;
	*(uintptr_t*)(args + __ARG_OFFSET_8__) = __arg8;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuus4u(uintptr_t __arg0, uintptr_t __arg1, __struct_4__ __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_4__*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuuu1(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuuu1u1(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, uint8_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;
	args[__ARG_OFFSET_3__].u64 = __arg3;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuuui4(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuuus4u(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, __struct_4__ __arg3, uintptr_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(__struct_4__*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuuuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuuuu1(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint8_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	args[__ARG_OFFSET_3__].u64 = __arg3;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_uuuuuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static void __N2M_v(const MethodInfo* method)
{
    
	constexpr int __TOTAL_ARG_SIZE__ = 1;

    StackObject args[__TOTAL_ARG_SIZE__];

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vi(intptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(intptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vi4(int32_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(int32_t*)(args + __ARG_OFFSET_0__) = __arg0;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vr4(float __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(float*)(args + __ARG_OFFSET_0__) = __arg0;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vs0s27(__struct_0__ __arg0, __struct_27__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_0__*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_27__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vs0s59(__struct_0__ __arg0, __struct_59__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_0__*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_59__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vs0u(__struct_0__ __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_0__*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vs106(__struct_106__ __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_106__*)(args + __ARG_OFFSET_0__) = __arg0;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vs4(__struct_4__ __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_4__*)(args + __ARG_OFFSET_0__) = __arg0;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vs44(__struct_44__ __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_44__*)(args + __ARG_OFFSET_0__) = __arg0;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vs44i4(__struct_44__ __arg0, int32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_44__*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vs44s44(__struct_44__ __arg0, __struct_44__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(__struct_44__*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_44__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vu(uintptr_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vu1(uint8_t __arg0, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	args[__ARG_OFFSET_0__].u64 = __arg0;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vu8u(uint64_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uint64_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui(uintptr_t __arg0, intptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(intptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui2(uintptr_t __arg0, int16_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui4i4(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui4i4i4(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui4i4i4i4i4(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(int32_t*)(args + __ARG_OFFSET_5__) = __arg5;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui4s13(uintptr_t __arg0, int32_t __arg1, __struct_13__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_13__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui4s3(uintptr_t __arg0, int32_t __arg1, __struct_3__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_3__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui4s6(uintptr_t __arg0, int32_t __arg1, __struct_6__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_6__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui4s9(uintptr_t __arg0, int32_t __arg1, __struct_9__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_9__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui4u(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui4uu(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui4uuu(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui8(uintptr_t __arg0, int64_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int64_t*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vui8ui8(uintptr_t __arg0, int64_t __arg1, uintptr_t __arg2, int64_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int64_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int64_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vur4(uintptr_t __arg0, float __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(float*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vur4r4(uintptr_t __arg0, float __arg1, float __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(float*)(args + __ARG_OFFSET_1__) = __arg1;
	*(float*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vur8(uintptr_t __arg0, double __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(double*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus0(uintptr_t __arg0, __struct_0__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_0__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus0s27(uintptr_t __arg0, __struct_0__ __arg1, __struct_27__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_0__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_27__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus0s59(uintptr_t __arg0, __struct_0__ __arg1, __struct_59__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_0__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_59__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus0u(uintptr_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_0__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus106(uintptr_t __arg0, __struct_106__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_106__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus123u(uintptr_t __arg0, __struct_123__ __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_123__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus123uu(uintptr_t __arg0, __struct_123__ __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_123__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus13(uintptr_t __arg0, __struct_13__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_13__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus19(uintptr_t __arg0, __struct_19__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_19__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus19u(uintptr_t __arg0, __struct_19__ __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_19__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus19ui4(uintptr_t __arg0, __struct_19__ __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_19__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus27(uintptr_t __arg0, __struct_27__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_27__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus3(uintptr_t __arg0, __struct_3__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_3__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus36(uintptr_t __arg0, __struct_36__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_36__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus36s108(uintptr_t __arg0, __struct_36__ __arg1, __struct_108__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_36__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_108__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus36s108u(uintptr_t __arg0, __struct_36__ __arg1, __struct_108__ __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_36__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_108__*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus36uu(uintptr_t __arg0, __struct_36__ __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_36__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus3u(uintptr_t __arg0, __struct_3__ __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_3__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus4(uintptr_t __arg0, __struct_4__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_4__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus44(uintptr_t __arg0, __struct_44__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_44__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus44i4(uintptr_t __arg0, __struct_44__ __arg1, int32_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_44__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus44s44(uintptr_t __arg0, __struct_44__ __arg1, __struct_44__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_44__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_44__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus6(uintptr_t __arg0, __struct_6__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_6__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus8(uintptr_t __arg0, __struct_8__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_8__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vus9(uintptr_t __arg0, __struct_9__ __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_9__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vutypedbyrefu(uintptr_t __arg0, Il2CppTypedRef __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(Il2CppTypedRef*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuu1(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuu1s104(uintptr_t __arg0, uint8_t __arg1, __struct_104__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;
	*(__struct_104__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuu2(uintptr_t __arg0, uint16_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuu4(uintptr_t __arg0, uint32_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuu8(uintptr_t __arg0, uint64_t __arg1, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint64_t*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuu8u(uintptr_t __arg0, uint64_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint64_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuui4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuui4i4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuus27(uintptr_t __arg0, uintptr_t __arg1, __struct_27__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_27__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuus4(uintptr_t __arg0, uintptr_t __arg1, __struct_4__ __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_4__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuuu1(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuuu1s104(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, __struct_104__ __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;
	*(__struct_104__*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuuu1u1(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, uint8_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;
	args[__ARG_OFFSET_3__].u64 = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuuu4(uintptr_t __arg0, uintptr_t __arg1, uint32_t __arg2, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uint32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuuui4(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuuui4uu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, uintptr_t __arg4, uintptr_t __arg5, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(uintptr_t*)(args + __ARG_OFFSET_5__) = __arg5;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuuus4(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, __struct_4__ __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(__struct_4__*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuuuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuuuu1(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint8_t __arg3, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	args[__ARG_OFFSET_3__].u64 = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_vuuuuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, const MethodInfo* method)
{
    
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;

    Interpreter::Execute(method, args, nullptr);
}


Native2ManagedMethodInfo hybridclr::interpreter::g_native2managedStub[] = 
{

	{"i1u", (Il2CppMethodPointer)__N2M_i1u},
	{"i1uu", (Il2CppMethodPointer)__N2M_i1uu},
	{"i2u", (Il2CppMethodPointer)__N2M_i2u},
	{"i2uu", (Il2CppMethodPointer)__N2M_i2uu},
	{"i4", (Il2CppMethodPointer)__N2M_i4},
	{"i4i4i4", (Il2CppMethodPointer)__N2M_i4i4i4},
	{"i4s6s6", (Il2CppMethodPointer)__N2M_i4s6s6},
	{"i4s9s9", (Il2CppMethodPointer)__N2M_i4s9s9},
	{"i4u", (Il2CppMethodPointer)__N2M_i4u},
	{"i4u8u8", (Il2CppMethodPointer)__N2M_i4u8u8},
	{"i4ui", (Il2CppMethodPointer)__N2M_i4ui},
	{"i4ui1", (Il2CppMethodPointer)__N2M_i4ui1},
	{"i4ui2", (Il2CppMethodPointer)__N2M_i4ui2},
	{"i4ui4", (Il2CppMethodPointer)__N2M_i4ui4},
	{"i4ui4i4", (Il2CppMethodPointer)__N2M_i4ui4i4},
	{"i4ui4i4i4", (Il2CppMethodPointer)__N2M_i4ui4i4i4},
	{"i4ui8", (Il2CppMethodPointer)__N2M_i4ui8},
	{"i4uii", (Il2CppMethodPointer)__N2M_i4uii},
	{"i4ur4", (Il2CppMethodPointer)__N2M_i4ur4},
	{"i4ur8", (Il2CppMethodPointer)__N2M_i4ur8},
	{"i4us0", (Il2CppMethodPointer)__N2M_i4us0},
	{"i4us0s0", (Il2CppMethodPointer)__N2M_i4us0s0},
	{"i4us10", (Il2CppMethodPointer)__N2M_i4us10},
	{"i4us10s10", (Il2CppMethodPointer)__N2M_i4us10s10},
	{"i4us13", (Il2CppMethodPointer)__N2M_i4us13},
	{"i4us16", (Il2CppMethodPointer)__N2M_i4us16},
	{"i4us17", (Il2CppMethodPointer)__N2M_i4us17},
	{"i4us18", (Il2CppMethodPointer)__N2M_i4us18},
	{"i4us19", (Il2CppMethodPointer)__N2M_i4us19},
	{"i4us22", (Il2CppMethodPointer)__N2M_i4us22},
	{"i4us25", (Il2CppMethodPointer)__N2M_i4us25},
	{"i4us26", (Il2CppMethodPointer)__N2M_i4us26},
	{"i4us27", (Il2CppMethodPointer)__N2M_i4us27},
	{"i4us3", (Il2CppMethodPointer)__N2M_i4us3},
	{"i4us4", (Il2CppMethodPointer)__N2M_i4us4},
	{"i4us57", (Il2CppMethodPointer)__N2M_i4us57},
	{"i4us59", (Il2CppMethodPointer)__N2M_i4us59},
	{"i4us6", (Il2CppMethodPointer)__N2M_i4us6},
	{"i4us6s6", (Il2CppMethodPointer)__N2M_i4us6s6},
	{"i4us9", (Il2CppMethodPointer)__N2M_i4us9},
	{"i4us9s9", (Il2CppMethodPointer)__N2M_i4us9s9},
	{"i4uu", (Il2CppMethodPointer)__N2M_i4uu},
	{"i4uu1", (Il2CppMethodPointer)__N2M_i4uu1},
	{"i4uu1u1", (Il2CppMethodPointer)__N2M_i4uu1u1},
	{"i4uu2", (Il2CppMethodPointer)__N2M_i4uu2},
	{"i4uu4", (Il2CppMethodPointer)__N2M_i4uu4},
	{"i4uu8", (Il2CppMethodPointer)__N2M_i4uu8},
	{"i4uu8u8", (Il2CppMethodPointer)__N2M_i4uu8u8},
	{"i4uui4", (Il2CppMethodPointer)__N2M_i4uui4},
	{"i4uui4i4", (Il2CppMethodPointer)__N2M_i4uui4i4},
	{"i4uui4i4i4", (Il2CppMethodPointer)__N2M_i4uui4i4i4},
	{"i4uui4i4u1", (Il2CppMethodPointer)__N2M_i4uui4i4u1},
	{"i4uui4i4ui4", (Il2CppMethodPointer)__N2M_i4uui4i4ui4},
	{"i4uui4i4ui4i4i4", (Il2CppMethodPointer)__N2M_i4uui4i4ui4i4i4},
	{"i4uui4i4ui4u1", (Il2CppMethodPointer)__N2M_i4uui4i4ui4u1},
	{"i4uui4u1", (Il2CppMethodPointer)__N2M_i4uui4u1},
	{"i4uui4ui4", (Il2CppMethodPointer)__N2M_i4uui4ui4},
	{"i4uui4ui4u1", (Il2CppMethodPointer)__N2M_i4uui4ui4u1},
	{"i4uuii4i4", (Il2CppMethodPointer)__N2M_i4uuii4i4},
	{"i4uus10i4i4", (Il2CppMethodPointer)__N2M_i4uus10i4i4},
	{"i4uus3i4i4", (Il2CppMethodPointer)__N2M_i4uus3i4i4},
	{"i4uus4i4i4", (Il2CppMethodPointer)__N2M_i4uus4i4i4},
	{"i4uus6i4i4", (Il2CppMethodPointer)__N2M_i4uus6i4i4},
	{"i4uus9i4i4", (Il2CppMethodPointer)__N2M_i4uus9i4i4},
	{"i4uuu", (Il2CppMethodPointer)__N2M_i4uuu},
	{"i4uuu1i4", (Il2CppMethodPointer)__N2M_i4uuu1i4},
	{"i4uuu1i4i4", (Il2CppMethodPointer)__N2M_i4uuu1i4i4},
	{"i4uuui4", (Il2CppMethodPointer)__N2M_i4uuui4},
	{"i4uuui4i4", (Il2CppMethodPointer)__N2M_i4uuui4i4},
	{"i4uuui4i4i4", (Il2CppMethodPointer)__N2M_i4uuui4i4i4},
	{"i8u", (Il2CppMethodPointer)__N2M_i8u},
	{"i8ui8i4", (Il2CppMethodPointer)__N2M_i8ui8i4},
	{"i8uu", (Il2CppMethodPointer)__N2M_i8uu},
	{"i8uuu", (Il2CppMethodPointer)__N2M_i8uuu},
	{"iuu", (Il2CppMethodPointer)__N2M_iuu},
	{"r4u", (Il2CppMethodPointer)__N2M_r4u},
	{"r4uu", (Il2CppMethodPointer)__N2M_r4uu},
	{"r8u", (Il2CppMethodPointer)__N2M_r8u},
	{"r8uu", (Il2CppMethodPointer)__N2M_r8uu},
	{"s0u", (Il2CppMethodPointer)__N2M_s0u},
	{"s0us117", (Il2CppMethodPointer)__N2M_s0us117},
	{"s0uus117", (Il2CppMethodPointer)__N2M_s0uus117},
	{"s105u", (Il2CppMethodPointer)__N2M_s105u},
	{"s111", (Il2CppMethodPointer)__N2M_s111},
	{"s111u", (Il2CppMethodPointer)__N2M_s111u},
	{"s123u", (Il2CppMethodPointer)__N2M_s123u},
	{"s13ui4", (Il2CppMethodPointer)__N2M_s13ui4},
	{"s15uu1", (Il2CppMethodPointer)__N2M_s15uu1},
	{"s16u", (Il2CppMethodPointer)__N2M_s16u},
	{"s16uu", (Il2CppMethodPointer)__N2M_s16uu},
	{"s17u", (Il2CppMethodPointer)__N2M_s17u},
	{"s17ui4i4i4i4i4i4i4i4", (Il2CppMethodPointer)__N2M_s17ui4i4i4i4i4i4i4i4},
	{"s17uu", (Il2CppMethodPointer)__N2M_s17uu},
	{"s19u", (Il2CppMethodPointer)__N2M_s19u},
	{"s22u", (Il2CppMethodPointer)__N2M_s22u},
	{"s22us22", (Il2CppMethodPointer)__N2M_s22us22},
	{"s22uu", (Il2CppMethodPointer)__N2M_s22uu},
	{"s36s36u", (Il2CppMethodPointer)__N2M_s36s36u},
	{"s36u", (Il2CppMethodPointer)__N2M_s36u},
	{"s36us36u", (Il2CppMethodPointer)__N2M_s36us36u},
	{"s3u", (Il2CppMethodPointer)__N2M_s3u},
	{"s3ui4", (Il2CppMethodPointer)__N2M_s3ui4},
	{"s4u", (Il2CppMethodPointer)__N2M_s4u},
	{"s56", (Il2CppMethodPointer)__N2M_s56},
	{"s56u", (Il2CppMethodPointer)__N2M_s56u},
	{"s56uu", (Il2CppMethodPointer)__N2M_s56uu},
	{"s6u", (Il2CppMethodPointer)__N2M_s6u},
	{"s6ui4", (Il2CppMethodPointer)__N2M_s6ui4},
	{"s74u", (Il2CppMethodPointer)__N2M_s74u},
	{"s8u", (Il2CppMethodPointer)__N2M_s8u},
	{"s9u", (Il2CppMethodPointer)__N2M_s9u},
	{"s9ui4", (Il2CppMethodPointer)__N2M_s9ui4},
	{"u", (Il2CppMethodPointer)__N2M_u},
	{"u1", (Il2CppMethodPointer)__N2M_u1},
	{"u1i4", (Il2CppMethodPointer)__N2M_u1i4},
	{"u1i4i", (Il2CppMethodPointer)__N2M_u1i4i},
	{"u1s6", (Il2CppMethodPointer)__N2M_u1s6},
	{"u1s9", (Il2CppMethodPointer)__N2M_u1s9},
	{"u1u", (Il2CppMethodPointer)__N2M_u1u},
	{"u1u2", (Il2CppMethodPointer)__N2M_u1u2},
	{"u1ui", (Il2CppMethodPointer)__N2M_u1ui},
	{"u1ui1", (Il2CppMethodPointer)__N2M_u1ui1},
	{"u1ui2", (Il2CppMethodPointer)__N2M_u1ui2},
	{"u1ui4", (Il2CppMethodPointer)__N2M_u1ui4},
	{"u1ui4i", (Il2CppMethodPointer)__N2M_u1ui4i},
	{"u1ui4i4", (Il2CppMethodPointer)__N2M_u1ui4i4},
	{"u1ui4u", (Il2CppMethodPointer)__N2M_u1ui4u},
	{"u1ui4u1", (Il2CppMethodPointer)__N2M_u1ui4u1},
	{"u1ui8", (Il2CppMethodPointer)__N2M_u1ui8},
	{"u1uii", (Il2CppMethodPointer)__N2M_u1uii},
	{"u1ur4", (Il2CppMethodPointer)__N2M_u1ur4},
	{"u1ur8", (Il2CppMethodPointer)__N2M_u1ur8},
	{"u1us0us0u", (Il2CppMethodPointer)__N2M_u1us0us0u},
	{"u1us10s10", (Il2CppMethodPointer)__N2M_u1us10s10},
	{"u1us123", (Il2CppMethodPointer)__N2M_u1us123},
	{"u1us125", (Il2CppMethodPointer)__N2M_u1us125},
	{"u1us13", (Il2CppMethodPointer)__N2M_u1us13},
	{"u1us16", (Il2CppMethodPointer)__N2M_u1us16},
	{"u1us17", (Il2CppMethodPointer)__N2M_u1us17},
	{"u1us18", (Il2CppMethodPointer)__N2M_u1us18},
	{"u1us19", (Il2CppMethodPointer)__N2M_u1us19},
	{"u1us19u", (Il2CppMethodPointer)__N2M_u1us19u},
	{"u1us19ui4", (Il2CppMethodPointer)__N2M_u1us19ui4},
	{"u1us22", (Il2CppMethodPointer)__N2M_u1us22},
	{"u1us23", (Il2CppMethodPointer)__N2M_u1us23},
	{"u1us25", (Il2CppMethodPointer)__N2M_u1us25},
	{"u1us26", (Il2CppMethodPointer)__N2M_u1us26},
	{"u1us27", (Il2CppMethodPointer)__N2M_u1us27},
	{"u1us3", (Il2CppMethodPointer)__N2M_u1us3},
	{"u1us30", (Il2CppMethodPointer)__N2M_u1us30},
	{"u1us31", (Il2CppMethodPointer)__N2M_u1us31},
	{"u1us35", (Il2CppMethodPointer)__N2M_u1us35},
	{"u1us36", (Il2CppMethodPointer)__N2M_u1us36},
	{"u1us3s3", (Il2CppMethodPointer)__N2M_u1us3s3},
	{"u1us3u", (Il2CppMethodPointer)__N2M_u1us3u},
	{"u1us4", (Il2CppMethodPointer)__N2M_u1us4},
	{"u1us42", (Il2CppMethodPointer)__N2M_u1us42},
	{"u1us44", (Il2CppMethodPointer)__N2M_u1us44},
	{"u1us45", (Il2CppMethodPointer)__N2M_u1us45},
	{"u1us46", (Il2CppMethodPointer)__N2M_u1us46},
	{"u1us4s4", (Il2CppMethodPointer)__N2M_u1us4s4},
	{"u1us57", (Il2CppMethodPointer)__N2M_u1us57},
	{"u1us59", (Il2CppMethodPointer)__N2M_u1us59},
	{"u1us6", (Il2CppMethodPointer)__N2M_u1us6},
	{"u1us6s6", (Il2CppMethodPointer)__N2M_u1us6s6},
	{"u1us8", (Il2CppMethodPointer)__N2M_u1us8},
	{"u1us9", (Il2CppMethodPointer)__N2M_u1us9},
	{"u1us9s9", (Il2CppMethodPointer)__N2M_u1us9s9},
	{"u1uu", (Il2CppMethodPointer)__N2M_u1uu},
	{"u1uu1", (Il2CppMethodPointer)__N2M_u1uu1},
	{"u1uu1u1", (Il2CppMethodPointer)__N2M_u1uu1u1},
	{"u1uu2", (Il2CppMethodPointer)__N2M_u1uu2},
	{"u1uu2i4", (Il2CppMethodPointer)__N2M_u1uu2i4},
	{"u1uu2u2i4", (Il2CppMethodPointer)__N2M_u1uu2u2i4},
	{"u1uu4", (Il2CppMethodPointer)__N2M_u1uu4},
	{"u1uu8", (Il2CppMethodPointer)__N2M_u1uu8},
	{"u1uui4", (Il2CppMethodPointer)__N2M_u1uui4},
	{"u1uuu", (Il2CppMethodPointer)__N2M_u1uuu},
	{"u1uuu1", (Il2CppMethodPointer)__N2M_u1uuu1},
	{"u1uuui4", (Il2CppMethodPointer)__N2M_u1uuui4},
	{"u2u", (Il2CppMethodPointer)__N2M_u2u},
	{"u2uu", (Il2CppMethodPointer)__N2M_u2uu},
	{"u2uu2", (Il2CppMethodPointer)__N2M_u2uu2},
	{"u4u", (Il2CppMethodPointer)__N2M_u4u},
	{"u4uu", (Il2CppMethodPointer)__N2M_u4uu},
	{"u8u", (Il2CppMethodPointer)__N2M_u8u},
	{"u8uu", (Il2CppMethodPointer)__N2M_u8uu},
	{"us3", (Il2CppMethodPointer)__N2M_us3},
	{"uu", (Il2CppMethodPointer)__N2M_uu},
	{"uui", (Il2CppMethodPointer)__N2M_uui},
	{"uui4", (Il2CppMethodPointer)__N2M_uui4},
	{"uui4ui4uu", (Il2CppMethodPointer)__N2M_uui4ui4uu},
	{"uui4uu", (Il2CppMethodPointer)__N2M_uui4uu},
	{"uui4uuu", (Il2CppMethodPointer)__N2M_uui4uuu},
	{"uui4uuuu", (Il2CppMethodPointer)__N2M_uui4uuuu},
	{"uui4uuuuuu", (Il2CppMethodPointer)__N2M_uui4uuuuuu},
	{"uui8", (Il2CppMethodPointer)__N2M_uui8},
	{"uus3", (Il2CppMethodPointer)__N2M_uus3},
	{"uus4", (Il2CppMethodPointer)__N2M_uus4},
	{"uuu", (Il2CppMethodPointer)__N2M_uuu},
	{"uuu1", (Il2CppMethodPointer)__N2M_uuu1},
	{"uuui4", (Il2CppMethodPointer)__N2M_uuui4},
	{"uuui4i4", (Il2CppMethodPointer)__N2M_uuui4i4},
	{"uuui4i4uu", (Il2CppMethodPointer)__N2M_uuui4i4uu},
	{"uuui4ui4uu", (Il2CppMethodPointer)__N2M_uuui4ui4uu},
	{"uuui4uuu", (Il2CppMethodPointer)__N2M_uuui4uuu},
	{"uuui4uuuu", (Il2CppMethodPointer)__N2M_uuui4uuuu},
	{"uuui4uuuuuu", (Il2CppMethodPointer)__N2M_uuui4uuuuuu},
	{"uuus4u", (Il2CppMethodPointer)__N2M_uuus4u},
	{"uuuu", (Il2CppMethodPointer)__N2M_uuuu},
	{"uuuu1", (Il2CppMethodPointer)__N2M_uuuu1},
	{"uuuu1u1", (Il2CppMethodPointer)__N2M_uuuu1u1},
	{"uuuui4", (Il2CppMethodPointer)__N2M_uuuui4},
	{"uuuus4u", (Il2CppMethodPointer)__N2M_uuuus4u},
	{"uuuuu", (Il2CppMethodPointer)__N2M_uuuuu},
	{"uuuuu1", (Il2CppMethodPointer)__N2M_uuuuu1},
	{"uuuuuu", (Il2CppMethodPointer)__N2M_uuuuuu},
	{"v", (Il2CppMethodPointer)__N2M_v},
	{"vi", (Il2CppMethodPointer)__N2M_vi},
	{"vi4", (Il2CppMethodPointer)__N2M_vi4},
	{"vr4", (Il2CppMethodPointer)__N2M_vr4},
	{"vs0s27", (Il2CppMethodPointer)__N2M_vs0s27},
	{"vs0s59", (Il2CppMethodPointer)__N2M_vs0s59},
	{"vs0u", (Il2CppMethodPointer)__N2M_vs0u},
	{"vs106", (Il2CppMethodPointer)__N2M_vs106},
	{"vs4", (Il2CppMethodPointer)__N2M_vs4},
	{"vs44", (Il2CppMethodPointer)__N2M_vs44},
	{"vs44i4", (Il2CppMethodPointer)__N2M_vs44i4},
	{"vs44s44", (Il2CppMethodPointer)__N2M_vs44s44},
	{"vu", (Il2CppMethodPointer)__N2M_vu},
	{"vu1", (Il2CppMethodPointer)__N2M_vu1},
	{"vu8u", (Il2CppMethodPointer)__N2M_vu8u},
	{"vui", (Il2CppMethodPointer)__N2M_vui},
	{"vui2", (Il2CppMethodPointer)__N2M_vui2},
	{"vui4", (Il2CppMethodPointer)__N2M_vui4},
	{"vui4i4", (Il2CppMethodPointer)__N2M_vui4i4},
	{"vui4i4i4", (Il2CppMethodPointer)__N2M_vui4i4i4},
	{"vui4i4i4i4i4", (Il2CppMethodPointer)__N2M_vui4i4i4i4i4},
	{"vui4s13", (Il2CppMethodPointer)__N2M_vui4s13},
	{"vui4s3", (Il2CppMethodPointer)__N2M_vui4s3},
	{"vui4s6", (Il2CppMethodPointer)__N2M_vui4s6},
	{"vui4s9", (Il2CppMethodPointer)__N2M_vui4s9},
	{"vui4u", (Il2CppMethodPointer)__N2M_vui4u},
	{"vui4uu", (Il2CppMethodPointer)__N2M_vui4uu},
	{"vui4uuu", (Il2CppMethodPointer)__N2M_vui4uuu},
	{"vui8", (Il2CppMethodPointer)__N2M_vui8},
	{"vui8ui8", (Il2CppMethodPointer)__N2M_vui8ui8},
	{"vur4", (Il2CppMethodPointer)__N2M_vur4},
	{"vur4r4", (Il2CppMethodPointer)__N2M_vur4r4},
	{"vur8", (Il2CppMethodPointer)__N2M_vur8},
	{"vus0", (Il2CppMethodPointer)__N2M_vus0},
	{"vus0s27", (Il2CppMethodPointer)__N2M_vus0s27},
	{"vus0s59", (Il2CppMethodPointer)__N2M_vus0s59},
	{"vus0u", (Il2CppMethodPointer)__N2M_vus0u},
	{"vus106", (Il2CppMethodPointer)__N2M_vus106},
	{"vus123u", (Il2CppMethodPointer)__N2M_vus123u},
	{"vus123uu", (Il2CppMethodPointer)__N2M_vus123uu},
	{"vus13", (Il2CppMethodPointer)__N2M_vus13},
	{"vus19", (Il2CppMethodPointer)__N2M_vus19},
	{"vus19u", (Il2CppMethodPointer)__N2M_vus19u},
	{"vus19ui4", (Il2CppMethodPointer)__N2M_vus19ui4},
	{"vus27", (Il2CppMethodPointer)__N2M_vus27},
	{"vus3", (Il2CppMethodPointer)__N2M_vus3},
	{"vus36", (Il2CppMethodPointer)__N2M_vus36},
	{"vus36s108", (Il2CppMethodPointer)__N2M_vus36s108},
	{"vus36s108u", (Il2CppMethodPointer)__N2M_vus36s108u},
	{"vus36uu", (Il2CppMethodPointer)__N2M_vus36uu},
	{"vus3u", (Il2CppMethodPointer)__N2M_vus3u},
	{"vus4", (Il2CppMethodPointer)__N2M_vus4},
	{"vus44", (Il2CppMethodPointer)__N2M_vus44},
	{"vus44i4", (Il2CppMethodPointer)__N2M_vus44i4},
	{"vus44s44", (Il2CppMethodPointer)__N2M_vus44s44},
	{"vus6", (Il2CppMethodPointer)__N2M_vus6},
	{"vus8", (Il2CppMethodPointer)__N2M_vus8},
	{"vus9", (Il2CppMethodPointer)__N2M_vus9},
	{"vutypedbyrefu", (Il2CppMethodPointer)__N2M_vutypedbyrefu},
	{"vuu", (Il2CppMethodPointer)__N2M_vuu},
	{"vuu1", (Il2CppMethodPointer)__N2M_vuu1},
	{"vuu1s104", (Il2CppMethodPointer)__N2M_vuu1s104},
	{"vuu2", (Il2CppMethodPointer)__N2M_vuu2},
	{"vuu4", (Il2CppMethodPointer)__N2M_vuu4},
	{"vuu8", (Il2CppMethodPointer)__N2M_vuu8},
	{"vuu8u", (Il2CppMethodPointer)__N2M_vuu8u},
	{"vuui4", (Il2CppMethodPointer)__N2M_vuui4},
	{"vuui4i4", (Il2CppMethodPointer)__N2M_vuui4i4},
	{"vuus27", (Il2CppMethodPointer)__N2M_vuus27},
	{"vuus4", (Il2CppMethodPointer)__N2M_vuus4},
	{"vuuu", (Il2CppMethodPointer)__N2M_vuuu},
	{"vuuu1", (Il2CppMethodPointer)__N2M_vuuu1},
	{"vuuu1s104", (Il2CppMethodPointer)__N2M_vuuu1s104},
	{"vuuu1u1", (Il2CppMethodPointer)__N2M_vuuu1u1},
	{"vuuu4", (Il2CppMethodPointer)__N2M_vuuu4},
	{"vuuui4", (Il2CppMethodPointer)__N2M_vuuui4},
	{"vuuui4uu", (Il2CppMethodPointer)__N2M_vuuui4uu},
	{"vuuus4", (Il2CppMethodPointer)__N2M_vuuus4},
	{"vuuuu", (Il2CppMethodPointer)__N2M_vuuuu},
	{"vuuuu1", (Il2CppMethodPointer)__N2M_vuuuu1},
	{"vuuuuu", (Il2CppMethodPointer)__N2M_vuuuuu},
	{nullptr, nullptr},
};

static int8_t __N2M_AdjustorThunk_i1uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int16_t __N2M_AdjustorThunk_i2uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int16_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4u(uintptr_t __arg0, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4ui1(uintptr_t __arg0, int8_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4ui2(uintptr_t __arg0, int16_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4ui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4ui4i4(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4ui8(uintptr_t __arg0, int64_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int64_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4ur4(uintptr_t __arg0, float __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(float*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4ur8(uintptr_t __arg0, double __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(double*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4us13(uintptr_t __arg0, __struct_13__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_13__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4us16(uintptr_t __arg0, __struct_16__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_16__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4us17(uintptr_t __arg0, __struct_17__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_17__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4us18(uintptr_t __arg0, __struct_18__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_18__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4us19(uintptr_t __arg0, __struct_19__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_19__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4us22(uintptr_t __arg0, __struct_22__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_22__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4us25(uintptr_t __arg0, __struct_25__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_25__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4us26(uintptr_t __arg0, __struct_26__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_26__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4us3(uintptr_t __arg0, __struct_3__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_3__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4us6s6(uintptr_t __arg0, __struct_6__ __arg1, __struct_6__ __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_6__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_6__*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4us9(uintptr_t __arg0, __struct_9__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_9__*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4us9s9(uintptr_t __arg0, __struct_9__ __arg1, __struct_9__ __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_9__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_9__*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4uu1(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4uu2(uintptr_t __arg0, uint16_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4uu4(uintptr_t __arg0, uint32_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4uu8(uintptr_t __arg0, uint64_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint64_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4uu8u8(uintptr_t __arg0, uint64_t __arg1, uint64_t __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint64_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uint64_t*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4uui4i4ui4i4i4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, int32_t __arg3, uintptr_t __arg4, int32_t __arg5, int32_t __arg6, int32_t __arg7, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __ARG_OFFSET_6__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;
	constexpr int __ARG_SIZE_6__ = (sizeof(__arg6) + 7)/8;
	constexpr int __ARG_OFFSET_7__ = __ARG_OFFSET_6__ + __ARG_SIZE_6__;
	constexpr int __ARG_SIZE_7__ = (sizeof(__arg7) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_7__ + __ARG_SIZE_7__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(int32_t*)(args + __ARG_OFFSET_5__) = __arg5;
	*(int32_t*)(args + __ARG_OFFSET_6__) = __arg6;
	*(int32_t*)(args + __ARG_OFFSET_7__) = __arg7;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4uuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int32_t __N2M_AdjustorThunk_i4uuui4i4i4(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, int32_t __arg4, int32_t __arg5, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __ARG_OFFSET_5__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;
	constexpr int __ARG_SIZE_5__ = (sizeof(__arg5) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_5__ + __ARG_SIZE_5__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(int32_t*)(args + __ARG_OFFSET_4__) = __arg4;
	*(int32_t*)(args + __ARG_OFFSET_5__) = __arg5;

    int32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static int64_t __N2M_AdjustorThunk_i8uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    int64_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static intptr_t __N2M_AdjustorThunk_iuu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    intptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static float __N2M_AdjustorThunk_r4uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    float ret; Interpreter::Execute(method, args, &ret); return ret;
}


static double __N2M_AdjustorThunk_r8uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    double ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_13__ __N2M_AdjustorThunk_s13ui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_13__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_15__ __N2M_AdjustorThunk_s15uu1(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    __struct_15__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_16__ __N2M_AdjustorThunk_s16uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_16__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_17__ __N2M_AdjustorThunk_s17uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_17__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_22__ __N2M_AdjustorThunk_s22u(uintptr_t __arg0, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_22__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_22__ __N2M_AdjustorThunk_s22us22(uintptr_t __arg0, __struct_22__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_22__*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_22__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_22__ __N2M_AdjustorThunk_s22uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_22__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_3__ __N2M_AdjustorThunk_s3u(uintptr_t __arg0, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_3__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_3__ __N2M_AdjustorThunk_s3ui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    __struct_3__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_6__ __N2M_AdjustorThunk_s6u(uintptr_t __arg0, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_6__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static __struct_9__ __N2M_AdjustorThunk_s9u(uintptr_t __arg0, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    __struct_9__ ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1u(uintptr_t __arg0, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1ui(uintptr_t __arg0, intptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(intptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1ui1(uintptr_t __arg0, int8_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1ui2(uintptr_t __arg0, int16_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1ui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1ui4i4(uintptr_t __arg0, int32_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1ui8(uintptr_t __arg0, int64_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int64_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1ur4(uintptr_t __arg0, float __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(float*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1ur8(uintptr_t __arg0, double __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(double*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us0us0u(uintptr_t __arg0, __struct_0__ __arg1, uintptr_t __arg2, __struct_0__ __arg3, uintptr_t __arg4, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_0__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(__struct_0__*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us123(uintptr_t __arg0, __struct_123__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_123__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us125(uintptr_t __arg0, __struct_125__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_125__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us13(uintptr_t __arg0, __struct_13__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_13__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us16(uintptr_t __arg0, __struct_16__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_16__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us17(uintptr_t __arg0, __struct_17__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_17__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us18(uintptr_t __arg0, __struct_18__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_18__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us19(uintptr_t __arg0, __struct_19__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_19__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us19ui4(uintptr_t __arg0, __struct_19__ __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_19__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us22(uintptr_t __arg0, __struct_22__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_22__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us23(uintptr_t __arg0, __struct_23__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_23__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us25(uintptr_t __arg0, __struct_25__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_25__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us26(uintptr_t __arg0, __struct_26__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_26__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us27(uintptr_t __arg0, __struct_27__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_27__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us3(uintptr_t __arg0, __struct_3__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_3__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us30(uintptr_t __arg0, __struct_30__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_30__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us31(uintptr_t __arg0, __struct_31__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_31__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us35(uintptr_t __arg0, __struct_35__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_35__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us36(uintptr_t __arg0, __struct_36__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_36__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us3s3(uintptr_t __arg0, __struct_3__ __arg1, __struct_3__ __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_3__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_3__*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us42(uintptr_t __arg0, __struct_42__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_42__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us44(uintptr_t __arg0, __struct_44__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_44__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us45(uintptr_t __arg0, __struct_45__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_45__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us46(uintptr_t __arg0, __struct_46__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_46__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us6(uintptr_t __arg0, __struct_6__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_6__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us8(uintptr_t __arg0, __struct_8__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_8__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1us9(uintptr_t __arg0, __struct_9__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_9__*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1uu1(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1uu2(uintptr_t __arg0, uint16_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1uu4(uintptr_t __arg0, uint32_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1uu8(uintptr_t __arg0, uint64_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uint64_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1uuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1uuu1(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint8_t __N2M_AdjustorThunk_u1uuui4(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    uint8_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint16_t __N2M_AdjustorThunk_u2u(uintptr_t __arg0, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    uint16_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint16_t __N2M_AdjustorThunk_u2uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint16_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint32_t __N2M_AdjustorThunk_u4uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint32_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uint64_t __N2M_AdjustorThunk_u8uu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uint64_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_AdjustorThunk_uu(uintptr_t __arg0, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_AdjustorThunk_uui(uintptr_t __arg0, intptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(intptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_AdjustorThunk_uui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_AdjustorThunk_uus4(uintptr_t __arg0, __struct_4__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_4__*)(args + __ARG_OFFSET_1__) = __arg1;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_AdjustorThunk_uuu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_AdjustorThunk_uuui4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_AdjustorThunk_uuus4u(uintptr_t __arg0, uintptr_t __arg1, __struct_4__ __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_4__*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_AdjustorThunk_uuuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_AdjustorThunk_uuuu1(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_AdjustorThunk_uuuus4u(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, __struct_4__ __arg3, uintptr_t __arg4, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(__struct_4__*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static uintptr_t __N2M_AdjustorThunk_uuuuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    uintptr_t ret; Interpreter::Execute(method, args, &ret); return ret;
}


static void __N2M_AdjustorThunk_vu(uintptr_t __arg0, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vui(uintptr_t __arg0, intptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(intptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vui4(uintptr_t __arg0, int32_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vui4s13(uintptr_t __arg0, int32_t __arg1, __struct_13__ __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_13__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vui4s3(uintptr_t __arg0, int32_t __arg1, __struct_3__ __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_3__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vui4u(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vui4uu(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vui4uuu(uintptr_t __arg0, int32_t __arg1, uintptr_t __arg2, uintptr_t __arg3, uintptr_t __arg4, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __ARG_OFFSET_4__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;
	constexpr int __ARG_SIZE_4__ = (sizeof(__arg4) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_4__ + __ARG_SIZE_4__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(int32_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;
	*(uintptr_t*)(args + __ARG_OFFSET_4__) = __arg4;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vus13(uintptr_t __arg0, __struct_13__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_13__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vus19(uintptr_t __arg0, __struct_19__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_19__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vus19ui4(uintptr_t __arg0, __struct_19__ __arg1, uintptr_t __arg2, int32_t __arg3, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_19__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(int32_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vus3(uintptr_t __arg0, __struct_3__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_3__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vus36(uintptr_t __arg0, __struct_36__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_36__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vus36s108(uintptr_t __arg0, __struct_36__ __arg1, __struct_108__ __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_36__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_108__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vus36s108u(uintptr_t __arg0, __struct_36__ __arg1, __struct_108__ __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_36__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_108__*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vus36uu(uintptr_t __arg0, __struct_36__ __arg1, uintptr_t __arg2, uintptr_t __arg3, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_36__*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(uintptr_t*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vus6(uintptr_t __arg0, __struct_6__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_6__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vus9(uintptr_t __arg0, __struct_9__ __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(__struct_9__*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vuu(uintptr_t __arg0, uintptr_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vuu1(uintptr_t __arg0, uint8_t __arg1, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	args[__ARG_OFFSET_1__].u64 = __arg1;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vuui4(uintptr_t __arg0, uintptr_t __arg1, int32_t __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(int32_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vuus4(uintptr_t __arg0, uintptr_t __arg1, __struct_4__ __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(__struct_4__*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vuuu(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vuuu1u1(uintptr_t __arg0, uintptr_t __arg1, uint8_t __arg2, uint8_t __arg3, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	args[__ARG_OFFSET_2__].u64 = __arg2;
	args[__ARG_OFFSET_3__].u64 = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vuuus4(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, __struct_4__ __arg3, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	*(__struct_4__*)(args + __ARG_OFFSET_3__) = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


static void __N2M_AdjustorThunk_vuuuu1(uintptr_t __arg0, uintptr_t __arg1, uintptr_t __arg2, uint8_t __arg3, const MethodInfo* method)
{
    __arg0 += sizeof(Il2CppObject);
	constexpr int __ARG_OFFSET_0__ = 0;
	constexpr int __ARG_SIZE_0__ = (sizeof(__arg0) + 7)/8;
	constexpr int __ARG_OFFSET_1__ = __ARG_OFFSET_0__ + __ARG_SIZE_0__;
	constexpr int __ARG_SIZE_1__ = (sizeof(__arg1) + 7)/8;
	constexpr int __ARG_OFFSET_2__ = __ARG_OFFSET_1__ + __ARG_SIZE_1__;
	constexpr int __ARG_SIZE_2__ = (sizeof(__arg2) + 7)/8;
	constexpr int __ARG_OFFSET_3__ = __ARG_OFFSET_2__ + __ARG_SIZE_2__;
	constexpr int __ARG_SIZE_3__ = (sizeof(__arg3) + 7)/8;
	constexpr int __TOTAL_ARG_SIZE__ = __ARG_OFFSET_3__ + __ARG_SIZE_3__;

    StackObject args[__TOTAL_ARG_SIZE__];
	*(uintptr_t*)(args + __ARG_OFFSET_0__) = __arg0;
	*(uintptr_t*)(args + __ARG_OFFSET_1__) = __arg1;
	*(uintptr_t*)(args + __ARG_OFFSET_2__) = __arg2;
	args[__ARG_OFFSET_3__].u64 = __arg3;

    Interpreter::Execute(method, args, nullptr);
}


NativeAdjustThunkMethodInfo hybridclr::interpreter::g_adjustThunkStub[] = 
{

	{"i1uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_i1uu},
	{"i2uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_i2uu},
	{"i4u", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4u},
	{"i4ui1", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4ui1},
	{"i4ui2", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4ui2},
	{"i4ui4", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4ui4},
	{"i4ui4i4", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4ui4i4},
	{"i4ui8", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4ui8},
	{"i4ur4", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4ur4},
	{"i4ur8", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4ur8},
	{"i4us13", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4us13},
	{"i4us16", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4us16},
	{"i4us17", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4us17},
	{"i4us18", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4us18},
	{"i4us19", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4us19},
	{"i4us22", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4us22},
	{"i4us25", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4us25},
	{"i4us26", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4us26},
	{"i4us3", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4us3},
	{"i4us6s6", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4us6s6},
	{"i4us9", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4us9},
	{"i4us9s9", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4us9s9},
	{"i4uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4uu},
	{"i4uu1", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4uu1},
	{"i4uu2", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4uu2},
	{"i4uu4", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4uu4},
	{"i4uu8", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4uu8},
	{"i4uu8u8", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4uu8u8},
	{"i4uui4i4ui4i4i4", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4uui4i4ui4i4i4},
	{"i4uuu", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4uuu},
	{"i4uuui4i4i4", (Il2CppMethodPointer)__N2M_AdjustorThunk_i4uuui4i4i4},
	{"i8uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_i8uu},
	{"iuu", (Il2CppMethodPointer)__N2M_AdjustorThunk_iuu},
	{"r4uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_r4uu},
	{"r8uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_r8uu},
	{"s13ui4", (Il2CppMethodPointer)__N2M_AdjustorThunk_s13ui4},
	{"s15uu1", (Il2CppMethodPointer)__N2M_AdjustorThunk_s15uu1},
	{"s16uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_s16uu},
	{"s17uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_s17uu},
	{"s22u", (Il2CppMethodPointer)__N2M_AdjustorThunk_s22u},
	{"s22us22", (Il2CppMethodPointer)__N2M_AdjustorThunk_s22us22},
	{"s22uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_s22uu},
	{"s3u", (Il2CppMethodPointer)__N2M_AdjustorThunk_s3u},
	{"s3ui4", (Il2CppMethodPointer)__N2M_AdjustorThunk_s3ui4},
	{"s6u", (Il2CppMethodPointer)__N2M_AdjustorThunk_s6u},
	{"s9u", (Il2CppMethodPointer)__N2M_AdjustorThunk_s9u},
	{"u1u", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1u},
	{"u1ui", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1ui},
	{"u1ui1", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1ui1},
	{"u1ui2", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1ui2},
	{"u1ui4", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1ui4},
	{"u1ui4i4", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1ui4i4},
	{"u1ui8", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1ui8},
	{"u1ur4", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1ur4},
	{"u1ur8", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1ur8},
	{"u1us0us0u", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us0us0u},
	{"u1us123", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us123},
	{"u1us125", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us125},
	{"u1us13", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us13},
	{"u1us16", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us16},
	{"u1us17", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us17},
	{"u1us18", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us18},
	{"u1us19", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us19},
	{"u1us19ui4", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us19ui4},
	{"u1us22", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us22},
	{"u1us23", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us23},
	{"u1us25", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us25},
	{"u1us26", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us26},
	{"u1us27", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us27},
	{"u1us3", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us3},
	{"u1us30", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us30},
	{"u1us31", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us31},
	{"u1us35", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us35},
	{"u1us36", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us36},
	{"u1us3s3", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us3s3},
	{"u1us42", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us42},
	{"u1us44", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us44},
	{"u1us45", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us45},
	{"u1us46", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us46},
	{"u1us6", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us6},
	{"u1us8", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us8},
	{"u1us9", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1us9},
	{"u1uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1uu},
	{"u1uu1", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1uu1},
	{"u1uu2", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1uu2},
	{"u1uu4", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1uu4},
	{"u1uu8", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1uu8},
	{"u1uuu", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1uuu},
	{"u1uuu1", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1uuu1},
	{"u1uuui4", (Il2CppMethodPointer)__N2M_AdjustorThunk_u1uuui4},
	{"u2u", (Il2CppMethodPointer)__N2M_AdjustorThunk_u2u},
	{"u2uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_u2uu},
	{"u4uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_u4uu},
	{"u8uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_u8uu},
	{"uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_uu},
	{"uui", (Il2CppMethodPointer)__N2M_AdjustorThunk_uui},
	{"uui4", (Il2CppMethodPointer)__N2M_AdjustorThunk_uui4},
	{"uus4", (Il2CppMethodPointer)__N2M_AdjustorThunk_uus4},
	{"uuu", (Il2CppMethodPointer)__N2M_AdjustorThunk_uuu},
	{"uuui4", (Il2CppMethodPointer)__N2M_AdjustorThunk_uuui4},
	{"uuus4u", (Il2CppMethodPointer)__N2M_AdjustorThunk_uuus4u},
	{"uuuu", (Il2CppMethodPointer)__N2M_AdjustorThunk_uuuu},
	{"uuuu1", (Il2CppMethodPointer)__N2M_AdjustorThunk_uuuu1},
	{"uuuus4u", (Il2CppMethodPointer)__N2M_AdjustorThunk_uuuus4u},
	{"uuuuu", (Il2CppMethodPointer)__N2M_AdjustorThunk_uuuuu},
	{"vu", (Il2CppMethodPointer)__N2M_AdjustorThunk_vu},
	{"vui", (Il2CppMethodPointer)__N2M_AdjustorThunk_vui},
	{"vui4", (Il2CppMethodPointer)__N2M_AdjustorThunk_vui4},
	{"vui4s13", (Il2CppMethodPointer)__N2M_AdjustorThunk_vui4s13},
	{"vui4s3", (Il2CppMethodPointer)__N2M_AdjustorThunk_vui4s3},
	{"vui4u", (Il2CppMethodPointer)__N2M_AdjustorThunk_vui4u},
	{"vui4uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_vui4uu},
	{"vui4uuu", (Il2CppMethodPointer)__N2M_AdjustorThunk_vui4uuu},
	{"vus13", (Il2CppMethodPointer)__N2M_AdjustorThunk_vus13},
	{"vus19", (Il2CppMethodPointer)__N2M_AdjustorThunk_vus19},
	{"vus19ui4", (Il2CppMethodPointer)__N2M_AdjustorThunk_vus19ui4},
	{"vus3", (Il2CppMethodPointer)__N2M_AdjustorThunk_vus3},
	{"vus36", (Il2CppMethodPointer)__N2M_AdjustorThunk_vus36},
	{"vus36s108", (Il2CppMethodPointer)__N2M_AdjustorThunk_vus36s108},
	{"vus36s108u", (Il2CppMethodPointer)__N2M_AdjustorThunk_vus36s108u},
	{"vus36uu", (Il2CppMethodPointer)__N2M_AdjustorThunk_vus36uu},
	{"vus6", (Il2CppMethodPointer)__N2M_AdjustorThunk_vus6},
	{"vus9", (Il2CppMethodPointer)__N2M_AdjustorThunk_vus9},
	{"vuu", (Il2CppMethodPointer)__N2M_AdjustorThunk_vuu},
	{"vuu1", (Il2CppMethodPointer)__N2M_AdjustorThunk_vuu1},
	{"vuui4", (Il2CppMethodPointer)__N2M_AdjustorThunk_vuui4},
	{"vuus4", (Il2CppMethodPointer)__N2M_AdjustorThunk_vuus4},
	{"vuuu", (Il2CppMethodPointer)__N2M_AdjustorThunk_vuuu},
	{"vuuu1u1", (Il2CppMethodPointer)__N2M_AdjustorThunk_vuuu1u1},
	{"vuuus4", (Il2CppMethodPointer)__N2M_AdjustorThunk_vuuus4},
	{"vuuuu1", (Il2CppMethodPointer)__N2M_AdjustorThunk_vuuuu1},
	{nullptr, nullptr},
};

//!!!}}CODE
