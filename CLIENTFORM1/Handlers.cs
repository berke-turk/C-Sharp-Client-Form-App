using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CLIENTFORM1
{
    class Handlers
    {
        public enum ServerHandler
        {
            Hello = 1, // Belki hello mesajı kullanırsak diye :)
            ImagePosition = 2 // Resim pozisyonu gönderiyorum!
        }

        public enum ClientHandler // Lazım olursa artık :d
        {
            Hello = 1,
            ImagePosition = 2
        }

        public class Packet
        {
            public int id; // Gidecek şahsın id'si :)
            public int type; // Gidecek verinin packet tipi
        }

        public class ImagePositionPacket : Packet
        {
            public int LEFT; // Sol
            public int RIGHT; // Sağ
            public int UP; // Yukarı
            public int DOWN; // Aşağı
        }

        public static ImagePositionPacket CreateImagePositionPacket(int _id, int _type, int _LEFT, int _RIGHT, int _UP, int _DOWN)
        {
            ImagePositionPacket packet = new ImagePositionPacket();
            packet.id = _id;
            packet.type = _type;
            packet.LEFT = _LEFT;
            packet.RIGHT = _RIGHT;
            packet.UP = _UP;
            packet.DOWN = _DOWN;
            return packet;
        }

        public static void Handle(string _json)
        {
            JToken token = JToken.Parse(_json);
            if((int)token["type"] == (int)ServerHandler.ImagePosition)
            {
                ImagePositionPacket packet = JsonConvert.DeserializeObject<ImagePositionPacket>(_json); // <-- Json stringi tekrar packete çeviriyoruz :)
                Client.POSITION_PICTURE_UPDATE(packet.LEFT, packet.RIGHT, packet.UP, packet.DOWN);
            }
        }

    }
}
