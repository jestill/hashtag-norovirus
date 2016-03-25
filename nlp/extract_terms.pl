#!/usr/bin/perl -w 
#
# extract_terms.pl                                          |
#                                                           |
#  AUTHOR: James C. Estill                                  |
# STARTED: 04/04/2016                                       |
# UPDATED: 04/04/2016                                       |
#
# extract_terms.pl -i fileToSearch -t TermsToSearch -o outfile
# 
# DESCRIPTION:                                              |
# Search twitter stream data for terms and return rows
# that match a search criteria. Initially we return
# redundant values.
#-----------------------------------------------------------

# Assumes input as

#MessageID|PersonaID|PersonID|PlatformID|MessageID|TimeOfMessage|Latitude|Longitude|Message|Provenance|NamedLocation


#-----------------------------+
# INCLUDES                    |
#-----------------------------+
use strict;
use Getopt::Long;
use Digest::MD5 qw(md5 md5_hex md5_base64);

#-----------------------------+
# VARIABLES                   |
#-----------------------------+
my $search_file;               # The file we search against
my $search_terms;              # The terms we are searching for
my $outfile;                   # The output file
my $outmod;                    # New column with identifier

#-----------------------------+
# COMMAND LINE OPTIONS        |
#-----------------------------+
my $ok = GetOptions(# REQUIRED OPTIONS
		    "t|terms=s"            => \$search_terms,
                    "i|infile=s"           => \$search_file,
		    "m|modfie-s"           => \$outmod,
		    "o|out=s"              => \$outfile,);

# Open things up

if ($search_terms) {
    open (TERMS, "<$search_terms") ||
	die "Can not open search term files:\n$search_file\n";
} else {
    die "Error: Identify a search term file for input with --terms option.\n ";
}

# Accept from STDIN if no infile given
if ($search_file) {
    open (INFILE, "<$search_file") ||
	die "Can not open infile:\n$search_file\n";
}
else {
    open (INFILE, "<&STDIN") ||
	die "Can not open STDIN for input\n"
}

# Write to stdout if no outfile given
if ($outfile) {
    open (OUT, ">$outfile") ||
	die "Can not open outfile:\n$outfile\n";
}
else {
    open (OUT, ">&STDOUT") ||
	die "Can not write to STDOUT\n";
}

# Load the search terms to an array
my @terms;
while (<TERMS>) {
    
    chomp;
    push @terms, $_;
    
}


# Check that the stuff is there
foreach my $term (@terms) {
    print STDERR "Searching $term\n";

    while (<INFILE>) {
	chomp;

#	print STDERR "\t$_\n";


	# PRINT OUT MATCHES HERE
	# with a message identifier and a term
	
	if ( $_ =~ /$term/ ) {

	    # The following is 
#	    print OUT $_."\n";

	    # SPLIT THE STRING

	    # NEED TO SWITCH THE FOLLOWING TO THE DELIM
#	    my @in_cols = split (/\t/, $_);	    
#	    my @in_cols = split (/\t/, $_);
	    # The following for pipe delim
	    my @in_cols = split (/\|/, $_);
	    
	    # NEED TO SWITCH THE FOLLOWING
	    # if the message id incoming is null, create one on the fly
	    my $message_id = "test-id";
	    
	    print OUT $message_id."\t".
		$term."\n";
	}
	
    }

    # REST THE FILE TO THE BEGINNING
    seek INFILE, 0, 0;
    
}

# The rewrite the intput file with identifiers

exit;

while (<INFILE>) {

    chomp;
    my @in_cols = split (/\|/, $_);


    print 
    
    
}



# Get the file we want to search against


# Write mathches to an outfile

# Outfile goal

# messageID -- value


# Close files
close (TERMS);
close (INFILE);
close (OUT);
