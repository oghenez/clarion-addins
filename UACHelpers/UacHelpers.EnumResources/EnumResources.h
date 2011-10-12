#pragma once

namespace UacHelpers {

	///<summary>
	///Represents a binary resource extracted from a binary image.
	///</summary>
	public value class FileResource
	{
	public:
		///<summary>
		///Gets or sets the resource type.
		///</summary>
		property System::String^ ResourceType;
		///<summary>
		///Gets or sets the resource name.
		///</summary>
		property System::String^ ResourceName;
		///<summary>
		///Gets or sets the resource value as an array of bytes.
		///</summary>
		property array<System::Byte>^ ResourceValue;

		///<summary>
		///Constructs a new instance of a resource using the specified
		///resource type, name and value.
		///</summary>
		///<param name="resourceType">The resource type.</param>
		///<param name="resourceName">The resource name.</param>
		///<param name="resourceValue">The resource value.</param>
		FileResource(System::String^ resourceType, System::String^ resourceName,
					 array<System::Byte>^ resourceValue)
		{
			ResourceType = resourceType;
			ResourceName = resourceName;
			ResourceValue = resourceValue;
		}
	};

	///<summary>
	///Provides a facility for enumerating the binary resources
	///associated with a binary image.
	///</summary>
	public ref class FileResources
	{
	private:
		System::String^ _fileName;

	public:
		///<summary>
		///Constructs a new instance of the <see cref="FileResources"/> class
		///using the specified file name.
		///</summary>
		///<param name="fileName">The file name to analyze.</param>
		FileResources(System::String^ fileName) : _fileName(fileName) {}

		///<summary>
		///Obtains a list of all binary resources in this executable.
		///</summary>
		///<returns>A list of all binary resources in this executable,
		///represented as an array of <see cref="FileResource"/> objects.</returns>
		array<FileResource>^ GetResources();
	};

}