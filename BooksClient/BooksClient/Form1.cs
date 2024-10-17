namespace BooksClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var http = new HttpClient();
            var json = await  http.GetStringAsync($" https://www.googleapis.com/books/v1/volumes?q={textBox1.Text}");

            var result = System.Text.Json.JsonSerializer.Deserialize<BooksResult>(json);

            dataGridView1.DataSource = result.items.Select(x => x.volumeInfo).ToList();
        }
    }
}
