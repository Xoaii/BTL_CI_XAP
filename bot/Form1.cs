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
        public long chatId = 6052997336; // Mk fix tr??c 1 c�i chat id l� t�i khu?n c?a mk! -> c�i n�y li�n quan ??n vi?c nh�ng ? b�n app

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
        /// h�m t?o: ko ki?u, tr�ng t�n v?i class
        /// </summary>
       
            
        public Form1()
        {
            InitializeComponent();

            // Th?ng QuanLyBanHanglv1_bot
            string token = "5915060756:AAFpvQZxNL65OJopg-LoBhspN4D35U33c3A";

            //Console.WriteLine("my token=" + token);

            botClient = new TelegramBotClient(token);  // T?o 1 th?ng bot 

            CancellationTokenSource cts = new CancellationTokenSource();  // Th?ng n�y ?? h?y j ?� ki?m so�t ch??ng tr�nh
            // CancellationTokenSource cts = new CancellationTokenSource(); L�m nh? n�y c?ng ?c n�??

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,  //h�m x? l� khi c� ng??i ch�t ??n ???c g?i m?i khi c� c?p nh?t m?i t? telegram API -> n� x? l� v� tr? v? kq  
                pollingErrorHandler: HandlePollingErrorAsync,   // H�m n�y s? l� l?i -> c� l?i l� g?i th?ng n�y
                receiverOptions: receiverOptions,  // Th?ng n�y ?c new ? tr�n k�a tham s? c�i ??t v? vi?c c?p nh?t m?i
                cancellationToken: cts.Token    // Th?ng n�y l� h?y cts.Token  -> h?y n� l�m j ?
                                                // T�m l?i: b?t ??u qu� tr�nh nh?n c?p nh?t t? Telegram API b?ng c�ch k�ch ho?t botClient
                                                // c�c c?p nh?t s? ???c x? l� b?i h�m HandleUpdateAsync.
                                                // N?u x?y ra l?i trong qu� tr�nh nh?n c?p nh?t, h�m HandlePollingErrorAsync s? ???c g?i ?? x? l� l?i. 
                                                // 2 th?ng sau l� t�y ch?n c?p nh?t.
            );

            Task<User> me = botClient.GetMeAsync(); // ???c s? d?ng ?? g?i m?t y�u c?u ??n Telegram API ?? l?y th�ng tin v? bot hi?n t?i.
            // => N?m ??u th?ng bot r?i.
            AddLog($"Th?ng bot: @{me.Result.Username}");

            //async l?p tr�nh b?t ??ng b?
            // Tr? v? ??i t??ng Task ?? 
            // V?y l� form 
            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                // botClient: Th?ng n�y mk t?o ? tr�n r?i: ???c s? d?ng ?? g?i c�c y�u c?u t?i Telegram API
                // update: ch?a th�ng tin v? c?p nh?t m?i nh?n ???c t? Telegram API. Update ch?a c�c th�ng tin nh? tin nh?n, s? ki?n nh�m, thay ??i tr?ng th�i, v.v.
                //          V?y l� th?ng botClient y�u c?u -> tr? k?t qu? v? th?ng Update!
                // cancellationToken: Th?ng n�y n� s? l� khi c� l?i -> N� k th?y ?c g?i nh?ng k c� n� l� l?i ?<>??? nani
                // Only process Message updates: https://core.telegram.org/bots/api#message
                bool ok = false;

                //kdl? bi?n <=> bi?n ?� c� th? nh?n NULL

                // Telegram.Bot.Types.Message l� m?t l?p ??i di?n cho m?t tin nh?n trong Telegram.
                // L?p n�y ch?a c�c th�ng tin v? tin nh?n, bao g?m n?i dung, ng??i g?i, ng??i nh?n, th?i gian g?i, v? tr�, h�nh ?nh, v.v.
                Telegram.Bot.Types.Message message = null; // d?u ? ?? c� th? g�n null 

                // update.Message l� ng??i d�ng nh?n 1 tin nh?n m?i t?i bot
                if (update.Message != null)  // N?u tin n?u th?ng update kh�ng ph?i l� null => c� c?p nh?t m?i:
                {
                    // message kh�ng ph?i l� string -> n� l� ??i t??ng ??i di?n cho 1 tin nh?n
                    message = update.Message;   // V� tao g�n th�ng tin update v�o th?ng ??i di?n cho tin nh?n n�y
                    ok = true;
                }
                // update.EditedMessage l� c� 1 tin nh?n ?� g?i t? tr??c r?i => song gi? n� click ph?i chu?t s?a tin nh?n -> tao c?ng n?m ??u ra x? l�
                if (update.EditedMessage != null)
                {
                    message = update.EditedMessage;
                    ok = true;
                }

                // N� k chui v�o 2 if ? tr�n <=> !false ho?c message == null => return; -> th?y ki?m tra k? qu� c?!
                if (!ok || message == null) return; //tho�t ngay

                string messageText = message.Text;
                if (messageText == null) return;  //ko ch?i v?i null

                chatId = message.Chat.Id;  //id c?a ng??i ch�t v?i bot

                AddLog($"{chatId}: {messageText}");  //show l�n ?? xem -> ch? k ph?i g?i v? telegram

                string reply = "";  //?�y l� text tr? l?i

                string messLow = messageText.ToLower(); // C� l? k c?n thi?t!




                // ----------- B?T ??U X? L� -----------------------------------------------------------------------------
                // -> bot n�y l� x? l� ch? ??ng khi ng??i chat ??n ? ?�y!
                // C�n x? l� m� t? ??ng B�O C�O 1 c�i j ?� khi Database thay ??i th� g?i con bot ? ch? thay ??i ?�!
                // -> B�y gi? ch? c?n X? l� d? li?u ?? t?o ra th?ng reply

                // 1. khi h?i v? an C?p:
                if (messLow.StartsWith("gv"))
                {
                    reply = "FeedBack Gi�o vi�n:?? M�n h?c l?p tr�nh Windows th?y ?? Duy C?p. Gi?ng qu� x� l� HAY!????";
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


                else // N?u k ph?i l� th?ng n�o ??c bi?t th� => h�t cho P?n nghe
                {
                    reply = "??T�i n�i p?n nghe: " + messageText;
                }


                // ----------- K?T TH�C X? L� -----------------------------------------------------------------------
                AddLog(reply); //show log to see




                // Echo received message text
                // => botClient.SendTextMessageAsync: => c�i h�m n�y l� h�m g?i tin nh?n v? telegram
                // N� ?c g?i v�o ?o?n cu?i c?a h�m HandleUpdateAsync m� h�m HandleUpdateAsync ???c kh?i t?o khi form_Load r?i.
                // M?i khi c� tin nh?n ??n h�m HandleUpdateAsync -> s? ?c g?i
                // N?u -> ngon th� n� ch?y ??n ?�y v� rep l?i b�n telegram c�n n?u k ?n th� n� ch?y v? h�m l?i HandlePollingErrorAsync
                Telegram.Bot.Types.Message sentMessage = await botClient.SendTextMessageAsync(
                           // H�m g?i tin nh?n ?i n�y c?n setting nh? sau:
                           chatId: chatId, // chatId bi?n n�y l?y ? tr�n kia r?i -> l?u id th?ng chat v?i mk ?? b�y gi? tr? l?i l?i n� ch?! chu?n ch?a
                           text: reply,    // rep l?i b�n telegram th� g�n v�o thu?c t�nh text => ? ?�y l� bi?n reply mk ?� x? l� d? li?u ? tr�n r?i <>
                           parseMode: ParseMode.Html  // =>  Bro d�ng c�ch ?�nh d?u v?n b?n HTML ?? th? hi?n text.
                                                      //parseMode: ParseMode.Markdown => th� n� c?ng l� 1 c�ch ?�nh d?u v?n b?n nh?ng n� k phong ph� nh? html
                      );

                //??c th�m v? ParseMode.Html t?i: https://core.telegram.org/bots/api#html-style
            }

            // ?�y l� h�m s? l� l?i -> c� l?i n� chui v�o h�m n�y
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
                AddLog("----       L?i r?i -> K r� l?i j  -----------");
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