using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;


namespace bot
{
    public partial class Form1 : Form
    {
        //6052997336
        public long chatId = 6052997336; // Mk fix tr??c 1 cái chat id là tài khu?n c?a mk! -> cái này liên quan ??n vi?c nhúng ? bên app

        int logCounter = 0;

        void AddLog(string msg)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.BeginInvoke((MethodInvoker)delegate ()
                {
                    AddLog(msg);
                });
            }
            else
            {
                logCounter++;
                if (logCounter > 100)
                {
                    txtLog.Clear();
                    logCounter = 0;
                }
                txtLog.AppendText(msg + "\r\n");
            }
            Console.WriteLine(msg);
        }

        /// <summary>
        /// hàm t?o: ko ki?u, trùng tên v?i class
        /// </summary>
       
            
        public Form1()
        {
            InitializeComponent();

            // Th?ng QuanLyBanHanglv1_bot
            string token = "5915060756:AAFpvQZxNL65OJopg-LoBhspN4D35U33c3A";

            //Console.WriteLine("my token=" + token);

            botClient = new TelegramBotClient(token);  // T?o 1 th?ng bot 

            CancellationTokenSource cts = new CancellationTokenSource();  // Th?ng này ?? h?y j ?ó ki?m soát ch??ng trình
            // CancellationTokenSource cts = new CancellationTokenSource(); LÀm nh? này c?ng ?c nè??

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,  //hàm x? lý khi có ng??i chát ??n ???c g?i m?i khi có c?p nh?t m?i t? telegram API -> nó x? lý và tr? v? kq  
                pollingErrorHandler: HandlePollingErrorAsync,   // Hàm này s? lý l?i -> có l?i là g?i th?ng này
                receiverOptions: receiverOptions,  // Th?ng này ?c new ? trên kìa tham s? cài ??t v? vi?c c?p nh?t m?i
                cancellationToken: cts.Token    // Th?ng này là h?y cts.Token  -> h?y nó làm j ?
                                                // Túm l?i: b?t ??u quá trình nh?n c?p nh?t t? Telegram API b?ng cách kích ho?t botClient
                                                // các c?p nh?t s? ???c x? lý b?i hàm HandleUpdateAsync.
                                                // N?u x?y ra l?i trong quá trình nh?n c?p nh?t, hàm HandlePollingErrorAsync s? ???c g?i ?? x? lý l?i. 
                                                // 2 th?ng sau là tùy ch?n c?p nh?t.
            );

            Task<User> me = botClient.GetMeAsync(); // ???c s? d?ng ?? g?i m?t yêu c?u ??n Telegram API ?? l?y thông tin v? bot hi?n t?i.
            // => N?m ??u th?ng bot r?i.
            AddLog($"Th?ng bot: @{me.Result.Username}");

            //async l?p trình b?t ??ng b?
            // Tr? v? ??i t??ng Task ?? 
            // V?y là form 
            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                // botClient: Th?ng này mk t?o ? trên r?i: ???c s? d?ng ?? g?i các yêu c?u t?i Telegram API
                // update: ch?a thông tin v? c?p nh?t m?i nh?n ???c t? Telegram API. Update ch?a các thông tin nh? tin nh?n, s? ki?n nhóm, thay ??i tr?ng thái, v.v.
                //          V?y là th?ng botClient yêu c?u -> tr? k?t qu? v? th?ng Update!
                // cancellationToken: Th?ng này nó s? lý khi có l?i -> Nó k th?y ?c g?i nh?ng k có nó là l?i ?<>??? nani
                // Only process Message updates: https://core.telegram.org/bots/api#message
                bool ok = false;

                //kdl? bi?n <=> bi?n ?ó có th? nh?n NULL

                // Telegram.Bot.Types.Message là m?t l?p ??i di?n cho m?t tin nh?n trong Telegram.
                // L?p này ch?a các thông tin v? tin nh?n, bao g?m n?i dung, ng??i g?i, ng??i nh?n, th?i gian g?i, v? trí, hình ?nh, v.v.
                Telegram.Bot.Types.Message message = null; // d?u ? ?? có th? gán null 

                // update.Message là ng??i dùng nh?n 1 tin nh?n m?i t?i bot
                if (update.Message != null)  // N?u tin n?u th?ng update không ph?i là null => có c?p nh?t m?i:
                {
                    // message không ph?i là string -> nó là ??i t??ng ??i di?n cho 1 tin nh?n
                    message = update.Message;   // Và tao gán thông tin update vào th?ng ??i di?n cho tin nh?n này
                    ok = true;
                }
                // update.EditedMessage là có 1 tin nh?n ?ã g?i t? tr??c r?i => song gi? nó click ph?i chu?t s?a tin nh?n -> tao c?ng n?m ??u ra x? lý
                if (update.EditedMessage != null)
                {
                    message = update.EditedMessage;
                    ok = true;
                }

                // Nó k chui vào 2 if ? trên <=> !false ho?c message == null => return; -> th?y ki?m tra k? quá c?!
                if (!ok || message == null) return; //thoát ngay

                string messageText = message.Text;
                if (messageText == null) return;  //ko ch?i v?i null

                chatId = message.Chat.Id;  //id c?a ng??i chát v?i bot

                AddLog($"{chatId}: {messageText}");  //show lên ?? xem -> ch? k ph?i g?i v? telegram

                string reply = "";  //?ây là text tr? l?i

                string messLow = messageText.ToLower(); // Có l? k c?n thi?t!




                // ----------- B?T ??U X? LÝ -----------------------------------------------------------------------------
                // -> bot này là x? lý ch? ??ng khi ng??i chat ??n ? ?ây!
                // Còn x? lý mà t? ??ng BÁO CÁO 1 cái j ?ó khi Database thay ??i thì g?i con bot ? ch? thay ??i ?ó!
                // -> Bây gi? ch? c?n X? lý d? li?u ?? t?o ra th?ng reply

                // 1. khi h?i v? an C?p:
                if (messLow.StartsWith("gv"))
                {
                    reply = "FeedBack Giáo viên:?? Môn h?c l?p trình Windows th?y ?? Duy C?p. Gi?ng quá xá là HAY!????";
                }
                else if (messLow.StartsWith("dh "))
                {
                    string soHD = messageText.Substring(3);

                }
                else if (messLow.StartsWith("timkh "))
                {
                    string tenKH = messageText.Substring(6);
                    Tim tim = new Tim();
                    reply = tenKH.timKH("" + tenKH.Replace(' ', '%') + "%");

                }


                else // N?u k ph?i là th?ng nào ??c bi?t thì => hát cho P?n nghe
                {
                    reply = "??Tôi nói p?n nghe: " + messageText;
                }


                // ----------- K?T THÚC X? LÝ -----------------------------------------------------------------------
                AddLog(reply); //show log to see




                // Echo received message text
                // => botClient.SendTextMessageAsync: => cái hàm này là hàm g?i tin nh?n v? telegram
                // Nó ?c g?i vào ?o?n cu?i c?a hàm HandleUpdateAsync mà hàm HandleUpdateAsync ???c kh?i t?o khi form_Load r?i.
                // M?i khi có tin nh?n ??n hàm HandleUpdateAsync -> s? ?c g?i
                // N?u -> ngon thì nó ch?y ??n ?ây và rep l?i bên telegram còn n?u k ?n thì nó ch?y v? hàm l?i HandlePollingErrorAsync
                Telegram.Bot.Types.Message sentMessage = await botClient.SendTextMessageAsync(
                           // Hàm g?i tin nh?n ?i này c?n setting nh? sau:
                           chatId: chatId, // chatId bi?n này l?y ? trên kia r?i -> l?u id th?ng chat v?i mk ?? bây gi? tr? l?i l?i nó ch?! chu?n ch?a
                           text: reply,    // rep l?i bên telegram thì gán vào thu?c tính text => ? ?ây là bi?n reply mk ?ã x? lý d? li?u ? trên r?i <>
                           parseMode: ParseMode.Html  // =>  Bro dùng cách ?ánh d?u v?n b?n HTML ?? th? hi?n text.
                                                      //parseMode: ParseMode.Markdown => thì nó c?ng là 1 cách ?ánh d?u v?n b?n nh?ng nó k phong phú nh? html
                      );

                //??c thêm v? ParseMode.Html t?i: https://core.telegram.org/bots/api#html-style
            }

            // ?ây là hàm s? lý l?i -> có l?i nó chui vào hàm này
            Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                //var ErrorMessage = exception switch
                //{
                //    ApiRequestException apiRequestException
                //        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n L?I NH? SAU:\n{apiRequestException.Message}",
                //     => exception.ToString()
                //};

                //AddLog(ErrorMessage);
                Console.WriteLine("Looi roi anh ouwi");
                AddLog("----       L?i r?i -> K rõ l?i j  -----------");
                return Task.CompletedTask;
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}