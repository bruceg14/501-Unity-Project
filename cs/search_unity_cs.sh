zcat /da?_data/basemaps/gz/b2fFullU*.s | fgrep -F "Assets/" | fgrep -F ".cs" | fgrep -v ".meta" | head -n 10000  > new_cs_blobs.txt

cat new_cs_blobs.txt | cut -d \; -f 1 | ~/lookup/getValues b2P | grep -v "bitbucket" > cs_projects.txt
