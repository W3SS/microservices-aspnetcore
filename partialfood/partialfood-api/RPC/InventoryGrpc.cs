// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: inventory.proto
#pragma warning disable 1591
#region Designer generated code

using System;
using System.Threading;
using System.Threading.Tasks;
using grpc = global::Grpc.Core;

namespace PartialFoods.Services {
  public static partial class InventoryManagement
  {
    static readonly string __ServiceName = "PartialFoods.Services.InventoryManagement";

    static readonly grpc::Marshaller<global::PartialFoods.Services.GetQuantityRequest> __Marshaller_GetQuantityRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::PartialFoods.Services.GetQuantityRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::PartialFoods.Services.GetQuantityResponse> __Marshaller_GetQuantityResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::PartialFoods.Services.GetQuantityResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::PartialFoods.Services.GetQuantityRequest, global::PartialFoods.Services.GetQuantityResponse> __Method_GetEffectiveQuantity = new grpc::Method<global::PartialFoods.Services.GetQuantityRequest, global::PartialFoods.Services.GetQuantityResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetEffectiveQuantity",
        __Marshaller_GetQuantityRequest,
        __Marshaller_GetQuantityResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::PartialFoods.Services.InventoryReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of InventoryManagement</summary>
    public abstract partial class InventoryManagementBase
    {
      public virtual global::System.Threading.Tasks.Task<global::PartialFoods.Services.GetQuantityResponse> GetEffectiveQuantity(global::PartialFoods.Services.GetQuantityRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for InventoryManagement</summary>
    public partial class InventoryManagementClient : grpc::ClientBase<InventoryManagementClient>
    {
      /// <summary>Creates a new client for InventoryManagement</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public InventoryManagementClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for InventoryManagement that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public InventoryManagementClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected InventoryManagementClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected InventoryManagementClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::PartialFoods.Services.GetQuantityResponse GetEffectiveQuantity(global::PartialFoods.Services.GetQuantityRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetEffectiveQuantity(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::PartialFoods.Services.GetQuantityResponse GetEffectiveQuantity(global::PartialFoods.Services.GetQuantityRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetEffectiveQuantity, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::PartialFoods.Services.GetQuantityResponse> GetEffectiveQuantityAsync(global::PartialFoods.Services.GetQuantityRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetEffectiveQuantityAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::PartialFoods.Services.GetQuantityResponse> GetEffectiveQuantityAsync(global::PartialFoods.Services.GetQuantityRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetEffectiveQuantity, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override InventoryManagementClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new InventoryManagementClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(InventoryManagementBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetEffectiveQuantity, serviceImpl.GetEffectiveQuantity).Build();
    }

  }
}
#endregion
