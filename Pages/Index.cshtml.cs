using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private HttpClient _httpClient;
        private Options _options;

        public IndexModel(HttpClient httpClient, Options options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        [BindProperty]
        public List<string> ImageList { get; private set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task OnGetAsync()
        {
            var imagesUrl = _options.ApiUrl;

            string imagesJson = await _httpClient.GetStringAsync(imagesUrl);

            IEnumerable<string> imagesList = JsonConvert.DeserializeObject<IEnumerable<string>>(imagesJson);

            this.ImageList = imagesList.ToList<string>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Upload != null && Upload.Length > 0)
            {
            // Server-side size check: 10 MB limit
            const long maxBytes = 10L * 1024 * 1024;
            if (Upload.Length > maxBytes)
            {
                ModelState.AddModelError("Upload", "ไม่ได้นะ ต้องอัพไฟล์ขนาดน้อยกว่า 10 mb");
                await OnGetAsync(); // repopulate ImageList for the page
                return Page();
            }
            if (!IsImage(Upload))
            {
                ModelState.AddModelError("Upload", "The uploaded file must be an image.");
                await OnGetAsync(); // repopulate ImageList for the page
                return Page();
            }

            var imagesUrl = _options.ApiUrl;

            using (var image = new StreamContent(Upload.OpenReadStream()))
            {
                image.Headers.ContentType = new MediaTypeHeaderValue(Upload.ContentType);
                var response = await _httpClient.PostAsync(imagesUrl, image);
            }
            }
            return RedirectToPage("/Index");
        }

        private bool IsImage(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null || file.Length == 0)
            return false;

            var ext = System.IO.Path.GetExtension(file.FileName ?? string.Empty).ToLowerInvariant();
            var allowedExt = new HashSet<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            return allowedExt.Contains(ext);
        }
    }
}