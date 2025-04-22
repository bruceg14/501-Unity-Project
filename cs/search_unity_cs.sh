# Adapted from previous code created by me during researching with Professor Hindle
zcat /da?_data/basemaps/gz/b2fFullU*.s | fgrep -F "Assets/" | fgrep -F ".cs" | fgrep -v ".meta" | head -n 10000  > new_cs_blobs.txt

input_file="new_cs_blobs.txt"
projects_file="cs_projects.txt"
final_output="cs_blob_filename_project.txt"

# Modified from my code in my research with Professor Hindle
while IFS=';' read -r blob filename; do
    echo "reading blob $blob"
    project_info=$(echo "$blob" | ~/lookup/getValues b2P)
    if [ ! -z "$project_info" ]; then
        echo "$blob;$filename;$(echo $project_info | cut -d ';' -f 2)" >> "$final_output"
    fi
done < "$input_file"


cat "$final_output" | cut -d ';' -f3 | sort -u > "$projects_file"

echo "Blob;Filename;Project" > formatted_output.txt

sort "$final_output" >> formatted_output.txt

awk -F ';' '{
  $3 = "github.com/" gensub("_", "/", "g", $3);
  print $1 ";" $2 ";" $3;
}' OFS=';' formatted_output.txt > updated_input_file.txt

awk -F ';' '{print $3}' formatted_output.txt | grep -v '^bitbucket' | sed 's/^/github.com\//; s/_/\//g' > github_projects.csv

USERNAME="bruceg14"
PAT=""
INPUT_FILE="updated_input_file.txt"
OUTPUT_DIR="downloaded_cs_files"

mkdir -p "$OUTPUT_DIR"

sanitize_repo_name() {
    echo "$1" | sed 's/[^a-zA-Z0-9._-]/_/g'
}

while IFS=';' read -r blob filename repo_url; do
    repo_name=$(sanitize_repo_name "$(basename "$repo_url")")
    repo_dir="$OUTPUT_DIR/$repo_name"
    echo "Cloning repository: $repo_url..."
    if ! git clone "https://$USERNAME:$PAT@$repo_url" "$repo_dir"; then 
        echo "Failed to clone https://$USERNAME:$PAT@$repo_url"
        continue
    fi
    file_dir="$OUTPUT_DIR/extracted"
    mkdir -p "$file_dir"
    
   
    # Check if specific file exist
    if [ -f "$repo_dir/$filename" ]; then
        base_filename=$(basename "$filename")
        cp "$repo_dir/$filename" "$file_dir/${repo_name}_${base_filename}"
        echo "Successfully copied: $filename as ${repo_name}_${base_filename}"
    else
        echo "File not found: $filename in $repo_url"
    fi
    
    # Remove the repository after
    echo "Removing repository: $repo_dir"
    rm -rf "$repo_dir"
    
done < "$INPUT_FILE"

echo "Download process completed. Files are in $OUTPUT_DIR/extracted"

