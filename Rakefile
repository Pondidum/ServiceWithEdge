require 'bundler/setup'
require 'albacore'

nugets_restore :restore do |n|
	n.exe = 'tools/nuget/nuget.exe'
	n.out = 'packages'
end

build :compile do |msb|
	msb.target = [ :clean, :rebuild ]
	msb.sln = 'ServiceWithEdge.sln'
end

task :default => [ :restore, :compile, :test ]
