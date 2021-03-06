// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: inventory.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace PartialFoods.Services {

  /// <summary>Holder for reflection information generated from inventory.proto</summary>
  public static partial class InventoryReflection {

    #region Descriptor
    /// <summary>File descriptor for inventory.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static InventoryReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg9pbnZlbnRvcnkucHJvdG8SFVBhcnRpYWxGb29kcy5TZXJ2aWNlcyIgChFH",
            "ZXRQcm9kdWN0UmVxdWVzdBILCgNTS1UYASABKAkiJwoTR2V0UXVhbnRpdHlS",
            "ZXNwb25zZRIQCghRdWFudGl0eRgBIAEoDSJHChBBY3Rpdml0eVJlc3BvbnNl",
            "EjMKCkFjdGl2aXRpZXMYASADKAsyHy5QYXJ0aWFsRm9vZHMuU2VydmljZXMu",
            "QWN0aXZpdHkinAEKCEFjdGl2aXR5EgsKA1NLVRgBIAEoCRIRCglUaW1lc3Rh",
            "bXAYAiABKAQSEAoIUXVhbnRpdHkYAyABKA0SOQoMQWN0aXZpdHlUeXBlGAQg",
            "ASgOMiMuUGFydGlhbEZvb2RzLlNlcnZpY2VzLkFjdGl2aXR5VHlwZRISCgpB",
            "Y3Rpdml0eUlEGAUgASgJEg8KB09yZGVySUQYBiABKAkqUgoMQWN0aXZpdHlU",
            "eXBlEgsKB1VOS05PV04QABIMCghSRVNFUlZFRBABEgwKCFJFTEVBU0VEEAIS",
            "CwoHU0hJUFBFRBADEgwKCFNUT0NLQUREEAQy5QEKE0ludmVudG9yeU1hbmFn",
            "ZW1lbnQSbAoUR2V0RWZmZWN0aXZlUXVhbnRpdHkSKC5QYXJ0aWFsRm9vZHMu",
            "U2VydmljZXMuR2V0UHJvZHVjdFJlcXVlc3QaKi5QYXJ0aWFsRm9vZHMuU2Vy",
            "dmljZXMuR2V0UXVhbnRpdHlSZXNwb25zZRJgCgtHZXRBY3Rpdml0eRIoLlBh",
            "cnRpYWxGb29kcy5TZXJ2aWNlcy5HZXRQcm9kdWN0UmVxdWVzdBonLlBhcnRp",
            "YWxGb29kcy5TZXJ2aWNlcy5BY3Rpdml0eVJlc3BvbnNlYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::PartialFoods.Services.ActivityType), }, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::PartialFoods.Services.GetProductRequest), global::PartialFoods.Services.GetProductRequest.Parser, new[]{ "SKU" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::PartialFoods.Services.GetQuantityResponse), global::PartialFoods.Services.GetQuantityResponse.Parser, new[]{ "Quantity" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::PartialFoods.Services.ActivityResponse), global::PartialFoods.Services.ActivityResponse.Parser, new[]{ "Activities" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::PartialFoods.Services.Activity), global::PartialFoods.Services.Activity.Parser, new[]{ "SKU", "Timestamp", "Quantity", "ActivityType", "ActivityID", "OrderID" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum ActivityType {
    [pbr::OriginalName("UNKNOWN")] Unknown = 0,
    [pbr::OriginalName("RESERVED")] Reserved = 1,
    [pbr::OriginalName("RELEASED")] Released = 2,
    [pbr::OriginalName("SHIPPED")] Shipped = 3,
    [pbr::OriginalName("STOCKADD")] Stockadd = 4,
  }

  #endregion

  #region Messages
  public sealed partial class GetProductRequest : pb::IMessage<GetProductRequest> {
    private static readonly pb::MessageParser<GetProductRequest> _parser = new pb::MessageParser<GetProductRequest>(() => new GetProductRequest());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GetProductRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PartialFoods.Services.InventoryReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetProductRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetProductRequest(GetProductRequest other) : this() {
      sKU_ = other.sKU_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetProductRequest Clone() {
      return new GetProductRequest(this);
    }

    /// <summary>Field number for the "SKU" field.</summary>
    public const int SKUFieldNumber = 1;
    private string sKU_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string SKU {
      get { return sKU_; }
      set {
        sKU_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GetProductRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GetProductRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (SKU != other.SKU) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (SKU.Length != 0) hash ^= SKU.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (SKU.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(SKU);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (SKU.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SKU);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GetProductRequest other) {
      if (other == null) {
        return;
      }
      if (other.SKU.Length != 0) {
        SKU = other.SKU;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            SKU = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class GetQuantityResponse : pb::IMessage<GetQuantityResponse> {
    private static readonly pb::MessageParser<GetQuantityResponse> _parser = new pb::MessageParser<GetQuantityResponse>(() => new GetQuantityResponse());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GetQuantityResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PartialFoods.Services.InventoryReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetQuantityResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetQuantityResponse(GetQuantityResponse other) : this() {
      quantity_ = other.quantity_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GetQuantityResponse Clone() {
      return new GetQuantityResponse(this);
    }

    /// <summary>Field number for the "Quantity" field.</summary>
    public const int QuantityFieldNumber = 1;
    private uint quantity_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint Quantity {
      get { return quantity_; }
      set {
        quantity_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GetQuantityResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GetQuantityResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Quantity != other.Quantity) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Quantity != 0) hash ^= Quantity.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Quantity != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Quantity);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Quantity != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Quantity);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GetQuantityResponse other) {
      if (other == null) {
        return;
      }
      if (other.Quantity != 0) {
        Quantity = other.Quantity;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Quantity = input.ReadUInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class ActivityResponse : pb::IMessage<ActivityResponse> {
    private static readonly pb::MessageParser<ActivityResponse> _parser = new pb::MessageParser<ActivityResponse>(() => new ActivityResponse());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ActivityResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PartialFoods.Services.InventoryReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ActivityResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ActivityResponse(ActivityResponse other) : this() {
      activities_ = other.activities_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ActivityResponse Clone() {
      return new ActivityResponse(this);
    }

    /// <summary>Field number for the "Activities" field.</summary>
    public const int ActivitiesFieldNumber = 1;
    private static readonly pb::FieldCodec<global::PartialFoods.Services.Activity> _repeated_activities_codec
        = pb::FieldCodec.ForMessage(10, global::PartialFoods.Services.Activity.Parser);
    private readonly pbc::RepeatedField<global::PartialFoods.Services.Activity> activities_ = new pbc::RepeatedField<global::PartialFoods.Services.Activity>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::PartialFoods.Services.Activity> Activities {
      get { return activities_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ActivityResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ActivityResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!activities_.Equals(other.activities_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= activities_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      activities_.WriteTo(output, _repeated_activities_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += activities_.CalculateSize(_repeated_activities_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ActivityResponse other) {
      if (other == null) {
        return;
      }
      activities_.Add(other.activities_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            activities_.AddEntriesFrom(input, _repeated_activities_codec);
            break;
          }
        }
      }
    }

  }

  public sealed partial class Activity : pb::IMessage<Activity> {
    private static readonly pb::MessageParser<Activity> _parser = new pb::MessageParser<Activity>(() => new Activity());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Activity> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PartialFoods.Services.InventoryReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Activity() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Activity(Activity other) : this() {
      sKU_ = other.sKU_;
      timestamp_ = other.timestamp_;
      quantity_ = other.quantity_;
      activityType_ = other.activityType_;
      activityID_ = other.activityID_;
      orderID_ = other.orderID_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Activity Clone() {
      return new Activity(this);
    }

    /// <summary>Field number for the "SKU" field.</summary>
    public const int SKUFieldNumber = 1;
    private string sKU_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string SKU {
      get { return sKU_; }
      set {
        sKU_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Timestamp" field.</summary>
    public const int TimestampFieldNumber = 2;
    private ulong timestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong Timestamp {
      get { return timestamp_; }
      set {
        timestamp_ = value;
      }
    }

    /// <summary>Field number for the "Quantity" field.</summary>
    public const int QuantityFieldNumber = 3;
    private uint quantity_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint Quantity {
      get { return quantity_; }
      set {
        quantity_ = value;
      }
    }

    /// <summary>Field number for the "ActivityType" field.</summary>
    public const int ActivityTypeFieldNumber = 4;
    private global::PartialFoods.Services.ActivityType activityType_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::PartialFoods.Services.ActivityType ActivityType {
      get { return activityType_; }
      set {
        activityType_ = value;
      }
    }

    /// <summary>Field number for the "ActivityID" field.</summary>
    public const int ActivityIDFieldNumber = 5;
    private string activityID_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string ActivityID {
      get { return activityID_; }
      set {
        activityID_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "OrderID" field.</summary>
    public const int OrderIDFieldNumber = 6;
    private string orderID_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string OrderID {
      get { return orderID_; }
      set {
        orderID_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Activity);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Activity other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (SKU != other.SKU) return false;
      if (Timestamp != other.Timestamp) return false;
      if (Quantity != other.Quantity) return false;
      if (ActivityType != other.ActivityType) return false;
      if (ActivityID != other.ActivityID) return false;
      if (OrderID != other.OrderID) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (SKU.Length != 0) hash ^= SKU.GetHashCode();
      if (Timestamp != 0UL) hash ^= Timestamp.GetHashCode();
      if (Quantity != 0) hash ^= Quantity.GetHashCode();
      if (ActivityType != 0) hash ^= ActivityType.GetHashCode();
      if (ActivityID.Length != 0) hash ^= ActivityID.GetHashCode();
      if (OrderID.Length != 0) hash ^= OrderID.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (SKU.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(SKU);
      }
      if (Timestamp != 0UL) {
        output.WriteRawTag(16);
        output.WriteUInt64(Timestamp);
      }
      if (Quantity != 0) {
        output.WriteRawTag(24);
        output.WriteUInt32(Quantity);
      }
      if (ActivityType != 0) {
        output.WriteRawTag(32);
        output.WriteEnum((int) ActivityType);
      }
      if (ActivityID.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(ActivityID);
      }
      if (OrderID.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(OrderID);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (SKU.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SKU);
      }
      if (Timestamp != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Timestamp);
      }
      if (Quantity != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Quantity);
      }
      if (ActivityType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ActivityType);
      }
      if (ActivityID.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ActivityID);
      }
      if (OrderID.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(OrderID);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Activity other) {
      if (other == null) {
        return;
      }
      if (other.SKU.Length != 0) {
        SKU = other.SKU;
      }
      if (other.Timestamp != 0UL) {
        Timestamp = other.Timestamp;
      }
      if (other.Quantity != 0) {
        Quantity = other.Quantity;
      }
      if (other.ActivityType != 0) {
        ActivityType = other.ActivityType;
      }
      if (other.ActivityID.Length != 0) {
        ActivityID = other.ActivityID;
      }
      if (other.OrderID.Length != 0) {
        OrderID = other.OrderID;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            SKU = input.ReadString();
            break;
          }
          case 16: {
            Timestamp = input.ReadUInt64();
            break;
          }
          case 24: {
            Quantity = input.ReadUInt32();
            break;
          }
          case 32: {
            activityType_ = (global::PartialFoods.Services.ActivityType) input.ReadEnum();
            break;
          }
          case 42: {
            ActivityID = input.ReadString();
            break;
          }
          case 50: {
            OrderID = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
