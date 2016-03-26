#!/usr/bin/perl -w
#-----------------------------------------------------------+
# extract_terms.pl                                          |
#                                                           |
#  AUTHOR: James C. Estill                                  |
# STARTED: 04/04/2016                                       |
# UPDATED: 04/25/2016                                       |
#                                                           |
# extract_terms.pl -i fileToSearch -t TermsToSearch         |
#                  -o outfile                               |
#                                                           |
# DESCRIPTION:                                              |
# Search twitter stream data for terms and return rows      |
# that match a search criteria. 
# This returns all matches                                  |
# from the search term set and will give multiple rows for  |
# text streams that have 
#-----------------------------------------------------------+


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
    # This is an ugly hack and requires reading through the entire input file
    # for each term that is being searched for.
    seek INFILE, 0, 0;
    
}


exit;

# The rewrite the intput file with identifiers
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


1;


1;
__END__

=head1 NAME

extract_terms.pl - Identify search terms from social media messages.

=head1 VERSION

This documentation refers to program version 0.1

=head1 SYNOPSIS

=head2 Usage

    extract_terms.pl -i file.csv -t terms.txt -o out.txt

=head2 Required Arguments

    --infile        # Path to the file containg the text to be searched
    --terms         # Path to the file containing terms to search for
    --out           # Path to the file that the output will be written to

=head1 DESCRIPTION

Searches social media stream data for terms and returns the rows that
match a given set of serach terms. Output will be a two column delimited
file that gives the ID of the individual social media message, and the term
used in the content of that message. This purpose is to add create a basic
structured data set from natural language phrases.

=head1 REQUIRED ARGUMENTS

=over 2

=item -i,--infile

Path of the input file that contains the rows of data that will be searched
for the search terms. If this option is not given, the programs assumes input 
will be coming from STDIN.

=item -o,--outfile

Path of the output file that the output text will be written to. If this option is
not given the program assumes that input will be coming from STDOUT.

=item -t,--terms

Path to the file containing the terms that will be searched for. Each term will
be search for exactly as typed, and each term should be entered on its own row
in the input file.

=back

=head1 OPTIONS

=over 2

=item --usage

Short overview of how to use program from command line.

=item --help

Show program usage with summary of options.

=item --version

Show program version.

=item --man

Show the full program manual. This uses the perldoc command to print the 
POD documentation for the program.

=item -q,--quiet

Run the program with minimal output to STDERR.

=back

=head1 EXAMPLES

The following are examples of how to use this script:

=head2 Typical Use

Typically you would use this program by specifying the input file that contains
the terms you are searching for:

  ./extract_terms.pl -i file.csv -t terms.txt -o outfile.txt

=head1 DIAGNOSTICS

=over 2

=item * Expecting input from STDIN

If you see this message, it may indicate that you did not properly specify
the input sequence with -i or --infile flag. However, you will see this 
notice when you are streaming data from standard input.

=back

=head1 CONFIGURATION AND ENVIRONMENT

This program does not currently make use of varibles set in the user environment.

This program currently expects input in a pipe delim format. With columns in the 
following order:

=over 2

=item * MessageID 

UniqueID for the message.

=item * PersonaID 

This is unique ID for the online persona for a person.

=item * PersonID 

The is the unique ID for a enity or person that the message derived from. 
A person can have one or more than one persona.

=item * PlatformID

This is the ID for the platform that the message derived from. 
For example Twitter, Facebook, YikYak.

=item * TimeOfMessage

The time the message was sent. Current no constraints around this.

=item * Latitude

The latitude the message was sent from if provided. Can be null.

=item * Longitude

The longitude the message was sent from if provided. Can be null.

=item * Message

The message string. This is the string that will be analyzed by the 
term extractor.

=item * Provenance

A string that provides the provenance for the message. This currently has 
no constrains on expected structure.

=item * NamedLocation

The location name for the place that the message originated from. This currently 
has no constraints on the expected structure, but generally should be a human
readable place name that could be recognized by a Business Intelligence
tool such as Tableau. This could include city locations such as 'Dallas, Texas'
or other location identifiers such as zip code.

=back

=head1 DEPENDENCIES

Currently this program is not dependent on applications or modules that are
outside of the base Perl installation.

=head1 BUGS AND LIMITATIONS

Any known bugs and limitations will be listed here.

The following are known limitations of the program

=over 2

=item * Message duplications are not accounted for

Thep program assumes that all messages are unique. Messages such as retweets will be 
returne multiple times.

=item * Unique IDs

The program currently assumes that all incoming messages have already been 
assigned unique identifiers.

=back


=head1 LICENSE

Apache 2.0

L<http://tinyrul.com/jamieapache>

=head1 AUTHOR

James C. Estill E<lt>jaestill at umich.eduE<gt>

=head1 HISTORY

STARTED: 04/04/2016

UPDATED: 04/25/2016

VERSION: $Rev$

=cut

#



