using UnityEngine;

public static class TransformSerializer
{
    public static void Register()
    {
        ExitGames.Client.Photon.PhotonPeer.RegisterType(typeof(Transform), (byte)'T', SerializeTransform, DeserializeTransform);
    }

    private static byte[] SerializeTransform(object i_customobject)
    {
        Transform tran = (Transform)i_customobject;

        var bytes = new byte[7 * sizeof(float)];
        int index = 0;
        ExitGames.Client.Photon.Protocol.Serialize(tran.position.x, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Serialize(tran.position.y, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Serialize(tran.position.z, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Serialize(tran.rotation.x, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Serialize(tran.rotation.y, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Serialize(tran.rotation.z, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Serialize(tran.rotation.w, bytes, ref index);

        return bytes;
    }

    private static object DeserializeTransform(byte[] i_bytes)
    {
        GameObject emptyGO = new GameObject();
        var tran = emptyGO.transform;
        float[] tmp = new float[7];
        int index = 0;
        ExitGames.Client.Photon.Protocol.Deserialize(out tmp[0], i_bytes, ref index);
        ExitGames.Client.Photon.Protocol.Deserialize(out tmp[1], i_bytes, ref index);
        ExitGames.Client.Photon.Protocol.Deserialize(out tmp[2], i_bytes, ref index);
        ExitGames.Client.Photon.Protocol.Deserialize(out tmp[3], i_bytes, ref index);
        ExitGames.Client.Photon.Protocol.Deserialize(out tmp[4], i_bytes, ref index);
        ExitGames.Client.Photon.Protocol.Deserialize(out tmp[5], i_bytes, ref index);
        ExitGames.Client.Photon.Protocol.Deserialize(out tmp[6], i_bytes, ref index);

        tran.position = new Vector3(tmp[0], tmp[1], tmp[2]);
        tran.rotation = new Quaternion(tmp[3], tmp[4], tmp[5], tmp[6]);
        return tran;
    }

}