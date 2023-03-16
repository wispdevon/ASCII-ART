# Set variables
background_color="black"
text_color_options=("blue" "yellow" "magenta" "cyan" "gray" "orange" "brown" "tan" "purple" "maroon" "navy" "aqua" "turquoise" "olive" "lime" "teal" "fuchsia" "silver" "gold" "pink" "green" "red" "white")
text_size=48
text_padding=60
size=512x512

random_font=false
font="azeretbold"
#major, space, oxygen, nova, share, noto, anon, anonbold, anonbolditalic, anonitalic, azeret, azeretbold, azeretsemibold
#best results: noto, anon, azeretbold
fontpathlist_path="./fonts/filelist.txt"
filelist_path="./ascii/filelist.txt"
num_lines=$(wc -l < $filelist_path)
output_path="./temp.png"
last_line_letter_file="./ascii/4emojis.txt"
emoji_font_path="./fonts/notocoloremoji_font.ttf"

if $random_font;
then 
font_path='$(shuf -n 1 $fontpathlist_path)'
else
font_path="./fonts/$font.ttf" 
fi

	
# ASCII generation, Magick method
command="magick -size $size xc:$background_color -gravity center"
for i in $(seq 1 $num_lines); do
    path=$(sed -n ${i}p $filelist_path)
    color=$(shuf -n 1 -e ${text_color_options[@]})
    y_pos=$((i*$text_padding - $text_padding*($num_lines+1)/2))
	text=$(shuf -n 1 $path)
    command+=" -font $font_path -fill $color -pointsize $text_size -annotate +0+$y_pos '$text'"
done

#-font notocoloremoji_font.ttf	

command+=" $output_path"

# Execute ImageMagick command
eval $command

file="$output_path"
hash=$(md5sum "$file" | cut -d' ' -f1)
mv "$file" ./output/''$hash'.png'