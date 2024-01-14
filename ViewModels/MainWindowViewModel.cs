using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Graven.Hearts.MakeGLTF.Constants;
using Graven.Hearts.MakeGLTF.Extensions;
using Graven.Hearts.MakeGLTF.Helpers;
using Graven.Hearts.MakeGLTF.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReactiveUI;

namespace Graven.Hearts.MakeGLTF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {   
        public static string AppName => App.AppName;
        public static string AppVersion => App.AppVersion.ToString(2);
        public static string AppBranding => App.AppBranding;
        public static string AppAuthor => App.AppAuthor;
        public static string AppOriginalCredits => App.AppOriginalCredits;

        public static string AppURL => App.AppURL;
        public static string AppOriginalURL => App.AppOriginalURL;

        public static int MaxWidthHD => 64;
        public static int MaxHeightHD => 64;

        public static int MaxWidth4K => 96;
        public static int MaxHeight4K => 96;

        public int CurrentMaxWidth
        {
            get => _currentMaxWidth;
            private set => this.RaiseAndSetIfChanged(ref _currentMaxWidth, value);
        }

        public int CurrentMaxHeight
        {
            get => _currentMaxHeight;
            private set => this.RaiseAndSetIfChanged(ref _currentMaxHeight, value);
        }

        public WriteableBitmap? BaseColourImage
        {
            get => _baseColourImage;

            private set
            {
                this.RaiseAndSetIfChanged(ref _baseColourImage, value);
                UpdateSaveState();
            }
        }

        public WriteableBitmap? NormalImage
        {
            get => _normalImage;

            private set
            {
                this.RaiseAndSetIfChanged(ref _normalImage, value);
                UpdateSaveState();
            }
        }

        public WriteableBitmap? AlphaImage
        {
            get => _alphaImage;

            private set
            {
                this.RaiseAndSetIfChanged(ref _alphaImage, value);
                UpdateClearState();
            }
        }

        public WriteableBitmap? OcclusionImage
        {
            get => _occlusionImage;

            private set
            {
                this.RaiseAndSetIfChanged(ref _occlusionImage, value);
                UpdateClearState();
            }
        }

        public WriteableBitmap? RoughnessImage
        {
            get => _roughnessImage;

            private set
            {
                this.RaiseAndSetIfChanged(ref _roughnessImage, value);
                UpdateClearState();
            }
        }

        public WriteableBitmap? MetallicImage
        {
            get => _metallicImage;

            private set
            {
                this.RaiseAndSetIfChanged(ref _metallicImage, value);
                UpdateClearState();
            }
        }

        public WriteableBitmap? EmissionImage
        {
            get => _emissionImage;

            private set
            {
                this.RaiseAndSetIfChanged(ref _emissionImage, value);
                UpdateClearState();
            }
        }

        public bool SaveEnabled
        {
            get => _saveEnabled;

            private set
            {
                this.RaiseAndSetIfChanged(ref _saveEnabled, value);
                UpdateClearState();
            }
        }

        public bool ClearEnabled
        {
            get => _clearEnabled;
            private set => this.RaiseAndSetIfChanged(ref _clearEnabled, value);
        }

        public bool ClearVisible
        {
            get => _clearVisible;
            private set => this.RaiseAndSetIfChanged(ref _clearVisible, value);
        }

        public string MaterialName
        {
            get => _materialName;
            private set => this.RaiseAndSetIfChanged(ref _materialName, value);
        }

        public string SaveName
        {
            get => _saveName;
            private set => this.RaiseAndSetIfChanged(ref _saveName, value);
        }

        private int _currentMaxWidth = MaxWidthHD;
        private int _currentMaxHeight = MaxHeightHD;

        private string BaseColourImagePath { get; set; } = string.Empty;
        private string NormalImagePath { get; set; } = string.Empty;
        private string AlphaImagePath { get; set; } = string.Empty;
        private string OcclusionImagePath { get; set; } = string.Empty;
        private string RoughnessImagePath { get; set; } = string.Empty;
        private string MetallicImagePath { get; set; } = string.Empty;
        private string EmissionImagePath { get; set; } = string.Empty;

        private WriteableBitmap? _baseColourImage;
        private WriteableBitmap? _normalImage;
        private WriteableBitmap? _alphaImage;
        private WriteableBitmap? _occlusionImage;
        private WriteableBitmap? _roughnessImage;
        private WriteableBitmap? _metallicImage;
        private WriteableBitmap? _emissionImage;

        private bool _saveEnabled = false;
        private bool _clearEnabled = false;
        private bool _clearVisible = true;

        private string _materialName = null!;

        private string _saveName = "Save";

        public void InitialiseForHD()
        {
            CurrentMaxWidth = MaxHeightHD;
            CurrentMaxHeight = MaxHeightHD;
        }

        public void InitialiseFor4K()
        {           
            CurrentMaxWidth = MaxHeight4K;
            CurrentMaxHeight = MaxHeight4K;
        }

        public void OnBaseColourImageChanged(string imagePath, WriteableBitmap? image)
        {
            if (BaseColourImage is not null)
            {
                BaseColourImage = null;
                return;
            }

            BaseColourImagePath = imagePath;
            BaseColourImage = image;
        }

        public void OnNormalImageChanged(string imagePath, WriteableBitmap? image)
        {
            if (NormalImage is not null)
            {
                NormalImage = null;
                return;
            }

            NormalImagePath = imagePath;
            NormalImage = image;
        }

        public void OnAlphaImageChanged(string imagePath, WriteableBitmap? image)
        {
            if (AlphaImage is not null)
            {
                AlphaImage = null;
                return;
            }

            AlphaImagePath = imagePath;
            AlphaImage = image;
        }

        public void OnOcclusionImageChanged(string imagePath, WriteableBitmap? image)
        {
            if (OcclusionImage is not null)
            {
                OcclusionImage = null;
                return;
            }

            OcclusionImagePath = imagePath;
            OcclusionImage = image;
        }

        public void OnRoughnessImageChanged(string imagePath, WriteableBitmap? image)
        {
            if (RoughnessImage is not null)
            {
                RoughnessImage = null;
                return;
            }

            RoughnessImagePath = imagePath;
            RoughnessImage = image;
        }

        public void OnMetallicImageChanged(string imagePath, WriteableBitmap? image)
        {
            if (MetallicImage is not null)
            {
                MetallicImage = null;
                return;
            }

            MetallicImagePath = imagePath;
            MetallicImage = image;
        }

        public void OnEmissionImageChanged(string imagePath, WriteableBitmap? image)
        {
            if (EmissionImage is not null)
            {
                EmissionImage = null;
                return;
            }

            EmissionImagePath = imagePath;
            EmissionImage = image;
        }

        public void OnClear()
        {
            BaseColourImage = NormalImage = AlphaImage = OcclusionImage =
            RoughnessImage = MetallicImage = EmissionImage = null;
            MaterialName = string.Empty;
        }

        public (string SaveName, bool SaveEnabled, bool ClearVisible) PrepareUIForSave()
        {
            var saveName = SaveName;
            var saveEnabled = SaveEnabled;
            var clearVisible = ClearVisible;

            SaveName = "Saving...";
            SaveEnabled = false;
            ClearVisible = false;

            return (saveName, saveEnabled, clearVisible);
        }

        public void OnSave()
        {
            if (BaseColourImage is null || NormalImage is null)
                return;

            var name = Assembly.GetExecutingAssembly().GetName().Name;
            using var stream = AssetLoader.Open(new Uri($"avares://{name}/Assets/template.gltf"));
            using var reader = new StreamReader(stream);
            var resourceData = reader.ReadToEnd();
            JObject gltfTemplate = JObject.Parse(resourceData);
            if (gltfTemplate is null)
                return;

            if (!gltfTemplate.TryGetValue("materials", out var materials) ||
                materials is not JArray materialsArray ||
                !materialsArray.HasValues ||
                materialsArray.FirstOrDefault() is not JObject selectedMaterial)
                return;

            if (!gltfTemplate.TryGetValue("textures", out var textures) ||
                textures is not JArray texturesArray ||
                !texturesArray.HasValues)
                return;

            if (!gltfTemplate.TryGetValue("images", out var images) ||
                images is not JArray imagesArray ||
                !imagesArray.HasValues)
                return;

            if (!gltfTemplate.TryGetValue("asset", out var asset))
                return;

            if (BaseColourImagePath.IsNullOrEmpty())
                return;

            var baseColourImageFolder = Path.GetDirectoryName(BaseColourImagePath);
            if (baseColourImageFolder.IsNullOrEmpty())
                return;

            var occlusionImage = OcclusionImage;
            occlusionImage ??= BitmapHelpers.SolidColourBitmap(255, 255, 255);

            var roughnessImage = RoughnessImage;
            roughnessImage ??= BitmapHelpers.SolidColourBitmap(255, 255, 255);

            var metallicImage = MetallicImage;
            if (metallicImage is not null)
            {
                selectedMaterial["metallicFactor"] = 1.0f;
            }
            else
            {
                metallicImage ??= BitmapHelpers.SolidColourBitmap(255, 255, 255);
                selectedMaterial["metallicFactor"] = 0.0f;
            }

            var gltfOutputFolder = Path.Combine(baseColourImageFolder, $"{MaterialName}");
            Directory.CreateDirectory(gltfOutputFolder);

            var ormFilePath = Path.Combine(gltfOutputFolder, $"{MaterialName}_orm.png");
            var orm = BitmapHelpers.CombineChannels(occlusionImage, roughnessImage, metallicImage, null!,
                BaseColourImage.PixelSize.Width, BaseColourImage.PixelSize.Height);

            orm.Save(ormFilePath, 100);

            var baseColourImage = BaseColourImage;

            if (AlphaImage is not null)
                baseColourImage = BitmapHelpers.CombineChannels(baseColourImage, baseColourImage, baseColourImage, AlphaImage,
                    baseColourImage.PixelSize.Width, baseColourImage.PixelSize.Height);

            var colFilePath = Path.Combine(gltfOutputFolder, $"{MaterialName}{FileExtConsts.Col}.png");
            baseColourImage.Save(colFilePath, 100);

            var nrmFilePath = Path.Combine(gltfOutputFolder, $"{MaterialName}_nrm.png");
            NormalImage.Save(nrmFilePath, 100);

            imagesArray[0]["mimeType"] = "image/png";
            imagesArray[0]["name"] = $"{MaterialName} (Normal)";
            imagesArray[0]["uri"] = Path.Combine(".", Path.GetFileName(nrmFilePath));

            imagesArray[1]["mimeType"] = "image/png";
            imagesArray[1]["name"] = $"{MaterialName} (Base Colour)";
            imagesArray[1]["uri"] = Path.Combine(".", Path.GetFileName(colFilePath));

            imagesArray[2]["mimeType"] = "image/png";
            imagesArray[2]["name"] = $"{MaterialName} (Occlusion, Roughness, Metallic)";
            imagesArray[2]["uri"] = Path.Combine(".", Path.GetFileName(ormFilePath));

            selectedMaterial["name"] = MaterialName;

            if (EmissionImage is not null)
            {
                var emissionFilePath = Path.Combine(gltfOutputFolder, $"{MaterialName}_emission.png");
                EmissionImage.Save(emissionFilePath, 100);

                imagesArray.Last?.AddAfterSelf(JToken.FromObject(new ImageToken($"{MaterialName} (Emission)",
                    Path.Combine(".", Path.GetFileName(emissionFilePath)))));

                texturesArray.Last?.AddAfterSelf(JToken.FromObject(new SourceToken(3)));
                selectedMaterial["emissiveTexture"] = JToken.FromObject(new IndexToken(3));
                var emissiveFactor = new float[] { 1.0f, 1.0f, 1.0f };
                selectedMaterial["emissiveFactor"] = JArray.FromObject(emissiveFactor);
            }

            selectedMaterial["doubleSided"] = false;

            asset["generator"] = $"{AppBranding} {AppName} v{AppVersion} {AppAuthor}";
            asset["version"] = AppVersion;

            var gltfFilePath = Path.Combine(gltfOutputFolder, $"{MaterialName}.gltf");
            File.WriteAllText(gltfFilePath, JsonConvert.SerializeObject(gltfTemplate, Formatting.Indented));
        }

        public void RestoreUIAfterSave((string SaveName, bool SaveEnabled, bool ClearVisible) orignalValues)
        {
            SaveName = orignalValues.SaveName;
            SaveEnabled = orignalValues.SaveEnabled;
            ClearVisible = orignalValues.ClearVisible;
        }

        public void AutoFillFromColor(string materialName, string folderName, string extension)
        {
            MaterialName = materialName;

            var searchPattern = $"{materialName}*{extension}";
            var files = Directory.GetFiles(folderName, searchPattern, new EnumerationOptions
            {
                MatchCasing = MatchCasing.CaseInsensitive
            });

            var normalsExtensions = new string[] { FileExtConsts.Normal, FileExtConsts.Norm, FileExtConsts.Nrml, FileExtConsts.Nrm, FileExtConsts.Nor };
            var occlusionExtensions = new string[] { FileExtConsts.Ambient, FileExtConsts.Occlusion, FileExtConsts.AO };
            var metallicExtensions = new string[] { FileExtConsts.Metallic, FileExtConsts.Metalness, FileExtConsts.Mtl };
            var roughnessExtensions = new string[] { FileExtConsts.Roughness, FileExtConsts.Rough, FileExtConsts.Roug, FileExtConsts.Rgh };
            var emissionExtensions = new string[] { FileExtConsts.Emission, FileExtConsts.Emiss, FileExtConsts.Emit };
            var alphaExtensions = new string[] { FileExtConsts.Alpha, FileExtConsts.Transparency };

            foreach (var file in files)
            {
                if (Path.GetFileNameWithoutExtension(file).EndsWithAnyIgnoringCase(normalsExtensions))
                    NormalImage = file.ReadImage();
                else
                if (Path.GetFileNameWithoutExtension(file).EndsWithAnyIgnoringCase(occlusionExtensions))
                    OcclusionImage = file.ReadImage();
                else
                if (Path.GetFileNameWithoutExtension(file).EndsWithAnyIgnoringCase(metallicExtensions))
                    MetallicImage = file.ReadImage();
                else
                if (Path.GetFileNameWithoutExtension(file).EndsWithAnyIgnoringCase(roughnessExtensions))
                    RoughnessImage = file.ReadImage();
                else
                if (Path.GetFileNameWithoutExtension(file).EndsWithAnyIgnoringCase(emissionExtensions))
                    EmissionImage = file.ReadImage();
                else
                if (Path.GetFileNameWithoutExtension(file).EndsWithAnyIgnoringCase(alphaExtensions))
                    AlphaImage = file.ReadImage();
            }
        }

        private void UpdateSaveState()
        {
            SaveEnabled = BaseColourImage is not null && NormalImage is not null;
            UpdateClearState();
        }

        private void UpdateClearState()
        {
            ClearEnabled = BaseColourImage is not null || NormalImage is not null ||
                AlphaImage is not null || OcclusionImage is not null ||
                RoughnessImage is not null || MetallicImage is not null ||
                EmissionImage is not null;
        }
    }
}
