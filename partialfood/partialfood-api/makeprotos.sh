PROJDIR=`pwd`
cd ~/.nuget/packages/grpc.tools/1.11.0/tools/linux
protoc -I $PROJDIR/../../proto --csharp_out $PROJDIR/RPC --grpc_out $PROJDIR/RPC $PROJDIR/../../proto/partialfoods.proto $PROJDIR/../../proto/inventory.proto $PROJDIR/../../proto/ordermgmt.proto --plugin=protoc-gen-grpc=grpc_csharp_plugin
cd -