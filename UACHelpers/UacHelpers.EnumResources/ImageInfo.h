#pragma once

namespace UacHelpers {

	///<summary>
	///Obtains executable image information from the PE headers.
	///</summary>
	///<remarks>
	///For .NET assemblies, it might be better to use FCL facilities
	///to extract this information (such as using the <see cref="AssemblyName"/> class).
	///</remarks>
	public ref class ImageInfo sealed
	{
	private:
		System::Reflection::ProcessorArchitecture _architecture;
	public:
		///<summary>
		///Constructs a new instance of the <see cref="ImageInfo"/> class
		///using the specified executable image.
		///</summary>
		///<param name="imageName">The image name.</param>
		ImageInfo(System::String^ imageName);
		
		///<summary>
		///Returns the processor architecture for the specified native
		///executable image.
		///</summary>
		property System::Reflection::ProcessorArchitecture Architecture
		{
			System::Reflection::ProcessorArchitecture get();
		};
	};

}
