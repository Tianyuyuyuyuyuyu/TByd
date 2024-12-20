# 使用 ImageMagick 将 SVG 转换为 ICO
# 需要先安装 ImageMagick: https://imagemagick.org/script/download.php#windows

# 确保输出目录存在
$outputDir = "windows/runner/resources"
if (-not (Test-Path $outputDir)) {
    New-Item -ItemType Directory -Force -Path $outputDir
}

# 使用新的命令格式转换为 ICO
magick "assets/icons/verdaccio.svg" -background none -define icon:auto-resize=256,128,64,48,32,16 "windows/runner/resources/app_icon.ico"