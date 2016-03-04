#!/usr/bin/perl -w 
#
# extract_terms.pl                                          |
#                                                           |
#  AUTHOR: James C. Estill                                  |
# STARTED: 04/04/2016                                       |
# UPDATED: 04/04/2016                                       |
# DESCRIPTION:                                              |
# Search twitter stream data for terms and return rows
# that match a search criteria. Initially we return
# redundant values.
#-----------------------------------------------------------


#-----------------------------+
# INCLUDES                    |
#-----------------------------+
use strict;
use Getopt::Long;


#-----------------------------+
# VARIABLES                   |
#-----------------------------+
my $search_file;               # The file we search against
my $search_terms;              # The terms we are searching for
my $outfile;                   # The output file

#-----------------------------+
# COMMAND LINE OPTIONS        |
#-----------------------------+
my $ok = GetOptions(# REQUIRED OPTIONS
		    "t|terms=s"            => \$search_terms,
                    "i|infile=s"           => \$search_file,
		    "o|out=s"              => \$outfile,);

# Open things up
open (TERMS, "<$search_terms") ||
    die "Can not open infile:\n$search_file\n";
open (INFILE, "<$search_file") ||
    die "Can not open infile:\n$search_file\n";
open (OUT, ">$outfile") ||
    die "Can not open outfile:\n$outfile\n";


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
	print STDERR "\t$_\n";


	# PRINT OUT MATCHES HERE
	
	if ( $_ =~ /$term/ ) {
	    print OUT $_."\n";
	}

	
    }
    
}


# Get the file we want to search against


# Write mathches to an outfile

# Outfile goal

# messageID -- value


# Close files
close (TERMS);
close (INFILE);
close (OUT);
