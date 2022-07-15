using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using TestCode.Model;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace TestCode.ViewModels
{
    public class VMJokes : Xamarin.CommunityToolkit.ObjectModel.ObservableObject
    {
        
        public VMJokes()
        {
            Init();
        }

        #region Properties

        public Action CallBackDone;

        private int idjokeSelect;

        private ObservableCollection<MJoke> _listJoke;

        public ObservableCollection<MJoke> ListJoke
        {
            get => _listJoke;
            set => SetProperty(ref _listJoke, value);
        }

        private string _content;

        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }
        #endregion

        #region Commands
        public Command FunnyCommand { get; set; }
        public Command DontFunnyCommand { get; set; }
        #endregion

        #region Actions
        void FunnyAction()
        {
            var item = ListJoke.Where(x => x.Id == idjokeSelect).FirstOrDefault();
            item.IsView = true;
            item.Evaluate = true;
            NextJoke();
        }

        void DontFunnyAction()
        {
            var item = ListJoke.Where(x => x.Id == idjokeSelect).FirstOrDefault();
            item.IsView = true;
            item.Evaluate =false;
            NextJoke();
        }
        #endregion

        #region Methods
        void Init()
        {
            _listJoke = new ObservableCollection<MJoke>();
            AddItem();
            //var item = ListJoke.FirstOrDefault();
            //Content = item.Content;
            //idjokeSelect = item.Id;
          
            FunnyCommand = new Command(FunnyAction);
            DontFunnyCommand = new Command(DontFunnyAction);
            //miniMaxSum2();
            RequestData();
        }

        void AddItem()
        {
            //ListJoke.Add(new MJoke { Id = 0, Content = $@"A child asked his father, ""How were people born?"" So his father said, ""Adam and Eve made babies, then their babies became adults and made babies, and so on.""The child then went to his mother,asked her the same question and she told him,""We were monkeys then we evolved to become like we are now.""The child ran back to his father and said, ""You lied to me!"" His father replied, ""No, your mom was talking about her side of the family.", IsView = false, Evaluate = null });
            //ListJoke.Add(new MJoke { Id = 1, Content = $@"Teacher: ""Kids, what does the chicken give you ? "" Student: ""Meat!"" Teacher: ""Very good! Now what does the pig give you ? "" Student: ""Bacon!"" Teacher: ""Great! And what does the fat cow give you ? "" Student: ""Homework!""", IsView = false, Evaluate = null });
            //ListJoke.Add(new MJoke { Id = 2, Content = $@"The teacher asked Jimmy, ""Why is your cat at school today Jimmy ? "" Jimmy replied crying, ""Because I heard my daddy tell my mommy, 'I am going to eat that pussy once Jimmy leaves for school today!'""", IsView = false, Evaluate = null });
            //ListJoke.Add(new MJoke { Id = 3, Content = $@"A housewife, an accountant and a lawyer were asked ""How much is 2 + 2 ? "" The housewife replies: ""Four!"". The accountant says: ""I think it's either 3 or 4. Let me run those figures through my spreadsheet one more time."" The lawyer pulls the drapes, dims the lights and asks in a hushed voice, ""How much do you want it to be?""", IsView = false, Evaluate = null });
        }

        public void NextJoke()
        {
            var item = ListJoke.Where(x => !x.IsView).FirstOrDefault();
            if(item != null)
            {
                Content = item.Content;
                idjokeSelect = item.Id;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Aler", "That's all the jokes for today! Come back another day!", "Ok");
            }
        }


        async void RequestData()
        {
            await Task.Run(async () =>
            {
                var model = new List<MJoke>();
                var rs = App.request.Requests(ref model, "Joke/GetListJoke","");
                Device.BeginInvokeOnMainThread(() => {
                    if (rs && model.Count > 0)
                    {
                        foreach (var item in model)
                        {
                            ListJoke.Add(item);
                        }
                    }
                    CallBackDone?.Invoke();
                });
            });
        }

        //  Solution one
        void miniMaxSum()
        {
            long[] A = {1,3,5,7,9};
            long max = A.Sum(), min = max;
            max -= A.Min();
            min -= A.Max();
            Console.WriteLine(min + " " + max);
        }

        //  Solution two
        void miniMaxSum2()
        {
            long[] arr = { 1, 3, 5, 7, 9 ,10 };
            long max = arr.Sum() - arr[0], min = arr.Sum() - arr[0];

            for (int i = 1; i < arr.Length; i++)
            {

                if (arr.Sum() - arr[i] > max) max = arr.Sum() - arr[i];

                if (arr.Sum() - arr[i] < min) min = arr.Sum() - arr[i];

            }

            string arrEven=string.Empty;
            string arrOdd = string.Empty;
            int Count = 0;
            foreach (var item in arr)
            {
                if (item % 2 == 0)
                {
                    arrEven += item.ToString() +" " ;
                }
                else
                {
                    arrOdd += item.ToString() + " ";
                }
                Count++;
            }

            Console.WriteLine("Count total of array {0}", arr.Count());

            Console.WriteLine("{0} {1}", min, max);

            Console.WriteLine("even elements in array {0}", arrEven);

            Console.WriteLine("Odd elements in array {0}", arrOdd);
        }
        #endregion
    }
}
