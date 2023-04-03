using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDPserver
{
    internal class Program
    {
        private static IPAddress remoteIPAddress;
        private static int remotePort;
        private static int localPort;
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Локальный порт:");
                localPort = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Удаленный порт:");
                remotePort = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Уажите IP-адресс");
                remoteIPAddress = IPAddress.Parse(Console.ReadLine());

                Thread tRec = new Thread(new ThreadStart(Receiver));
                tRec.Start();


                while (true)
                {
                    Send(Console.ReadLine());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение " + ex.ToString() + "\n" + ex.Message);
            }
        }
        private static void Send(string datagram)
        {
            UdpClient sender = new UdpClient();

            IPEndPoint endPoint = new IPEndPoint(remoteIPAddress, remotePort);


            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(datagram);

                sender.Send(bytes, bytes.Length, endPoint);

            }
            catch (ArgumentOutOfRangeException ex2)
            {
                Console.WriteLine("Некорректный номер порт");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение " + ex.ToString() + "\n" + ex.Message);
            }

            finally
            {
                sender.Close();
            }
        }

        public static void Receiver()
        {
            UdpClient receivingUpdClient = new UdpClient(localPort);

            IPEndPoint RemoteIpEndPoint = null;

            try
            {
                Console.WriteLine("\n ----------********----------");
                

                while (true)
                {
                    byte[] receiverButes = receivingUpdClient.Receive(ref RemoteIpEndPoint);


                    string returnData = Encoding.UTF8.GetString(receiverButes);
                    Console.WriteLine("-->" + returnData.ToString());
                    if (returnData =="Info" ) 
                    {
                        Console.WriteLine("1.Курячого філе");
                        Console.WriteLine("2.Свинина");
                        Console.WriteLine("3.Картопля");
                        Console.WriteLine("4.Риба");
                    }
                    if (returnData == "Курячого філе")
                    {
                        Console.WriteLine("1.Стріпси курячі:\nПотрібно: куряча грудка - 2 половинки, жменя борошна, кукурудзяні пластівці (не дивуйтесь, саме солодкі пластівці) – упаковка маленька, 1 яйце, сіль – дрібка, спеції: солодка паприка, сушений томат, сушений часник, петрушка свіжа – невеликий пучок.");
                        Console.WriteLine("2.Філе у маринаді солодкий чилі:\nЗнадобляться: 2 половинки філе, кілька гілочок кінзи/петрушки, олія, соус солодкий чилі 100 г, соєвий соус 3 ст. л, сік лимона 1 ч.л., часник 1 зубчик.");
                        Console.WriteLine("3.Куряче м'ясо по-французьки:\nНеобхідно: 3 половинки філе, кілька ложок сметани, 1 велика цибуля, 1 великий помідор, 100 гр твердого сиру, за бажанням: гриби шампіньйони");
                    }
                    if (returnData == "Свинина")
                    {
                        Console.WriteLine("1.Свинина, запечена по-французьки в духовці:\nІнгредієнти: 620 г корейки без кісточки, 2 свіжих помідори, майонез за смаком, 2 шт. цибулі-ріпки, сіль, мелений перець, 90 г сиру.");
                        Console.WriteLine("2.Медальйони з м’яса свинини:\nІнгредієнти: півкіло вирізки, 70 г масла вершкового, 2 пучка кмину, сіль.");
                        Console.WriteLine("3.Свинячий карбонат у фользі в духовці:\nІнгредієнти: 620 г карбонату свинячого, 2 головки цибулі, 2 ч. л. горошку запашного перцю 1 ч. л. меленого, щіпка коріандру в зернах, сіль, 2 гілочки чебрецю.");
                    }
                    if (returnData == "Картопля")
                    {
                        Console.WriteLine("1.Деруни:\nІнгредієнти:\r\n\r\nкартопля 6-7 шт.,\r\nцибулина,\r\nпечериці 300 г,\r\n2 зуб. часнику,\r\nсіль, перець,\r\nяйце,\r\n1 ст.л.борошна,\r\nпівсклянки сметани,\r\n100 г твердого сиру.");
                        Console.WriteLine("2.Запіканка з тертої картоплі:\nІнгредієнти:\r\nкартопля – 6 шт.;\r\nяйця – 2 шт.;\r\nчасник – 2 зуб.;\r\nсметана жирна – 3-4 ст. л.;\r\nсир твердий – 100 г;\r\nсушений кріп – 1 ст. л.\r\nсіль, перець – за смаком.");
                        Console.WriteLine("3.Картопляний гратен:\nІнгредієнти:\r\nкартопля – 5-6шт.\r\nвершки – 200 мл\r\nмускатний горіх – 1 ч.л.\r\nсіль, перець, спеції – за смаком");
                    }
                    if (returnData == "Риба")
                    {
                        Console.WriteLine("1.РИБНІ ПАЛИЧКИ:\nСКЛАДОВІ\r\n2 філе минтая\r\n1,5 склянки панірувальних сухарів\r\n150 г борошна\r\n1 яйце\r\n1 жовток\r\n200 мл натурального йогурту\r\n10 гілочок петрушки (або кропу)\r\n1 ст. л. лимонного соку\r\nсіль та перець за смаком");
                        Console.WriteLine("2.СВЯТКОВИЙ ПИРІГ З ГОРБУШЕЮ:\nСКЛАДОВІ\r\n2 філе лосося Аляски (однакового розміру)\r\n200 г шпинату\r\n4 ст. л. зеленого песто\r\n250 г печеного консервованого червоного перцю (розсіл попередньо злити та розрізати шматочки навпіл)\r\n2 листи листкового тіста (попередньо розморозити, якщо тісто заморожене)\r\n100 г вершкового масла, розтопленого\r\nсіль\r\nсвіжомелений чорний перець");
                        Console.WriteLine("3.РИБНІ КОТЛЕТИ:\nСКЛАДОВІ\r\n2 філе минтая\r\n3 ст. л. панірувальних сухарів\r\n200 мл води\r\n1 яєчний жовток\r\n1 цибуля\r\n1 морква\r\n1 картоплина (велика)\r\n1-2 буряки (залежно від розміру)\r\n100 г твердого сиру\r\n0,5 ч. л. куркуми\r\n1 ст. л. олії\r\n1 ч. л. соку лимона\r\nсіль та перець за смаком");
                    }
                }
            }
            catch (ArgumentOutOfRangeException ex2)
            {
                Console.WriteLine("Некорректный номер порт");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение " + ex.ToString() + "\n" + ex.Message);
            }
        }
    }
}
